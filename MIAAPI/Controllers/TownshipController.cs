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
    [Route("api/Township")]
    public class TownshipController : Controller
    {
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<Township> Get()
        {
            using (var db = My.ConnectionFactory())
            {
                return db.Query<Township>(My.Table_Township.Select).ToList();
            }
        }

        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        public Township Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {
                return db.QuerySingle<Township>(My.Table_Township.SelectSingle, new { townshipId = id });
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]Township value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_Township.SelectSingle}) {My.Table_Township.Update} ELSE {My.Table_Township.Insert}", value);
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
                    int result = db.Execute(My.Table_Township.Delete, new { townshipId = id });
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