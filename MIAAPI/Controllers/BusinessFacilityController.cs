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
    [Route("api/BusinessFacility")]
    [EnableCors("MyPolicy")]
    public class BusinessFacilityController : Controller
    {
        [HttpGet("{businessId}")]
        public IEnumerable<BusinessFacility> Get(string businessId)
        {
            using (var db = My.ConnectionFactory())
            {
                var businessFacilities = db.Query<BusinessFacility, Facility, BusinessFacility>($@"{My.Table_BusinessFacility.Select} 
            INNER JOIN dbo.Facility ON dbo.BusinessFacility.facilityId = dbo.Facility.facilityId
            WHERE (dbo.BusinessFacility.businessId = '{businessId}')", (businessFacility, facility) =>
                {
                    businessFacility.facility = facility;
                    return businessFacility;
                }, splitOn: "facilityId").ToList();

                return businessFacilities;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Business value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    db.Execute("DELETE FROM BusinessFacility WHERE [businessId]=@businessId", new { businessId = value.businessId });                    
                    foreach (var bf in value.businessFacility)
                    {
                        int result = db.Execute(My.Table_BusinessFacility.Insert, bf);
                    }                    
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid? id)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute(My.Table_BusinessFacility.Delete, new { businessFacilityId = id });
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