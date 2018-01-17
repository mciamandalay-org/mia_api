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
    [Route("api/Reason")]
    public class ReasonController : Controller
    {
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<Reason> Get()
        {
            using (var db = My.ConnectionFactory())
            {                
                return db.Query<Reason>(My.Table_Reason.Select).ToList();
            }
        }

        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        public Reason Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {                
                return db.QuerySingle<Reason>(My.Table_Reason.SelectSingle, new { reasonId=id });
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]Reason value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_Reason.SelectSingle}) {My.Table_Reason.Update} ELSE {My.Table_Reason.Insert}", value);
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
                    int result = db.Execute(My.Table_Reason.Delete, new { reasonId = id });
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