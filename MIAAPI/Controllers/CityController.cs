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
    [Route("api/City")]
    public class CityController : Controller
    {
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<City> Get()
        {
            using (var db = My.ConnectionFactory())
            {                
                return db.Query<City>(My.Table_City.Select).ToList();
            }
        }

        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        public City Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {                
                return db.QuerySingle<City>(My.Table_City.SelectSingle, new { cityId=id });
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]City value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_City.SelectSingle}) {My.Table_City.Update} ELSE {My.Table_City.Insert}", value);
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
                    int result = db.Execute(My.Table_City.Delete, new { cityId = id });
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