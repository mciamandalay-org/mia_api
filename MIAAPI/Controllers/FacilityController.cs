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
    [Route("api/Facility")]
    public class FacilityController : Controller
    {
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<Facility> Get()
        {
            using (var db = My.ConnectionFactory())
            {
                return db.Query<Facility>(My.Table_Facility.Select).ToList();
            }
        }

        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        public Facility Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {
                return db.QuerySingle<Facility>(My.Table_Facility.SelectSingle, new { facilityId=id });
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]Facility value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_Facility.SelectSingle}) {My.Table_Facility.Update} ELSE {My.Table_Facility.Insert}", value);
                }
                return Ok();
            }
            catch(Exception ex)
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
                    int result = db.Execute(My.Table_Facility.Delete, new { facilityId = id });
                    if (result > 0) return Ok(); else return NotFound();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }

}