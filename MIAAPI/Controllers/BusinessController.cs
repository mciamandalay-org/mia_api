using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MIAAPI.Models;
using MIAAPI.Utilities;
using Microsoft.AspNetCore.Cors;

namespace MIAAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Business")]
    [EnableCors("MyPolicy")]
    public class BusinessController : Controller
    {
        [HttpGet]
        public IEnumerable<Business> Get()
        {
            using (var db = My.ConnectionFactory())
            {
                var businesses = db.Query<Business, IndustryType, Township, City, BusinessType, Business>($@"{My.Table_Business.Select}
                LEFT OUTER JOIN dbo.IndustryType ON dbo.Business.industryTypeId = dbo.IndustryType.industryTypeId 
                LEFT OUTER JOIN dbo.Township ON dbo.Business.townshipId = dbo.Township.townshipId 
                LEFT OUTER JOIN dbo.City ON dbo.Business.cityId = dbo.City.cityId 
                LEFT OUTER JOIN dbo.BusinessType ON dbo.Business.businessTypeId = dbo.BusinessType.businessTypeId", 
                (business, industryType, township, city, businessType) =>
                {
                    if (business.industryTypeId!= null) business.industryType = industryType;
                    if (business.cityId != null) business.city = city;
                    if (business.townshipId != null) business.township = township;
                    if (business.businessTypeId != null) business.businessType = businessType;

                    return business;
                }, splitOn: "industryTypeId,townshipId,cityId,businessTypeId").ToList();

                return businesses;
            }
        }

        [HttpGet("{businessId}")]
        public Business Get(string businessId)
        {
            using (var db = My.ConnectionFactory())
            {
                var businesses = db.Query<Business, IndustryType, Township, City, BusinessType, Business>($@"{My.Table_Business.Select}
                LEFT OUTER JOIN dbo.IndustryType ON dbo.Business.industryTypeId = dbo.IndustryType.industryTypeId 
                LEFT OUTER JOIN dbo.Township ON dbo.Business.townshipId = dbo.Township.townshipId 
                LEFT OUTER JOIN dbo.City ON dbo.Business.cityId = dbo.City.cityId 
                LEFT OUTER JOIN dbo.BusinessType ON dbo.Business.businessTypeId = dbo.BusinessType.businessTypeId WHERE dbo.Business.businessId='{businessId}'",
                (business, industryType, township, city, businessType) =>
                {
                    if (business.industryTypeId != null) business.industryType = industryType;
                    if (business.cityId != null) business.city = city;
                    if (business.townshipId != null) business.township = township;
                    if (business.businessTypeId != null) business.businessType = businessType;

                    BusinessBranchController bbc = new BusinessBranchController();
                    business.businessBranch = bbc.Get(businessId).ToArray();

                    BusinessFacilityController bfc = new BusinessFacilityController();
                    business.businessFacility = bfc.Get(businessId).ToArray();

                    return business;
                }, splitOn: "industryTypeId,townshipId,cityId,businessTypeId").ToArray();

                //if (businesses != null) return businesses[0]; else return null;
                return (businesses.Length==1) ? businesses[0] : null;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Business value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_Business.SelectSingle}) {My.Table_Business.Update} ELSE {My.Table_Business.Insert}", value);

                    BusinessFacilityController businessFacility = new BusinessFacilityController();
                    businessFacility.Post(value);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [EnableCors("MyPolicy")]
        public IActionResult Delete(Guid? id)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute(My.Table_Business.Delete, new { businessId = id });
                    if (result > 0) return Ok(); else return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}