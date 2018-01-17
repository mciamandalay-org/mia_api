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
    [Route("api/Position")]
    public class PositionController : Controller
    {
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<Position> Get()
        {
            using (var db = My.ConnectionFactory())
            {                
                return db.Query<Position>(My.Table_Position.Select).ToList();
            }
        }

        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        public Position Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {                
                return db.QuerySingle<Position>(My.Table_Position.SelectSingle, new { positionId=id });
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]Position value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_Position.SelectSingle}) {My.Table_Position.Update} ELSE {My.Table_Position.Insert}", value);
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
                    int result = db.Execute(My.Table_Position.Delete, new { positionId = id });
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