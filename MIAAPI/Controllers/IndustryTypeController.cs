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
    [Route("api/IndustryType")]
    public class IndustryTypeController : Controller
    {
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<IndustryType> Get()
        {
            using (var db = My.ConnectionFactory())
            {
                return db.Query<IndustryType>(My.Table_IndustryType.Select).ToList();
            }
        }

        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        public IndustryType Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {
                return db.QuerySingle<IndustryType>(My.Table_IndustryType.SelectSingle, new { industryTypeId=id });
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]IndustryType value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_IndustryType.SelectSingle}) {My.Table_IndustryType.Update} ELSE {My.Table_IndustryType.Insert}", value);
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
                    int result = db.Execute(My.Table_IndustryType.Delete, new { industryTypeId = id });
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