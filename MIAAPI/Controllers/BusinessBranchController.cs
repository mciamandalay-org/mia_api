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
    [Route("api/BusinessBranch")]
    [EnableCors("MyPolicy")]
    public class BusinessBranchController : Controller
    {
        [HttpGet("{businessId}")]
        public IEnumerable<BusinessBranch> Get(string businessId)
        {
            using (var db = My.ConnectionFactory())
            {
                var businessBranch = db.Query<BusinessBranch, Township, City, BusinessBranch>($@"{My.Table_BusinessBranch.Select} 
                LEFT OUTER JOIN dbo.Township ON dbo.BusinessBranch.townshipId = dbo.Township.townshipId 
                LEFT OUTER JOIN dbo.City ON dbo.BusinessBranch.cityId = dbo.City.cityId
                WHERE (dbo.BusinessBranch.businessId = '{businessId}')", (branch, township, city) =>
                {
                    if (branch.townshipId != null) branch.township = township;
                    if (branch.cityId != null) branch.city = city;                    

                    return branch;
                }, splitOn: "townshipId,cityId").ToList();

                return businessBranch;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Business value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    foreach(var v in value.businessBranch)
                    {
                        int result = db.Execute($@"IF EXISTS({My.Table_BusinessBranch.SelectSingle}) {My.Table_BusinessBranch.Update} ELSE {My.Table_BusinessBranch.Insert}", v);
                    }                    
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid? id)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute(My.Table_BusinessBranch.Delete, new { businessBranchId = id });
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