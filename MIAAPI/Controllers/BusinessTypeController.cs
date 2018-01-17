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
    [Route("api/BusinessType")]
    public class BusinessTypeController : Controller
    {
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<BusinessType> Get()
        {
            using (var db = My.ConnectionFactory())
            {
                return db.Query<BusinessType>(My.Table_BusinessType.Select).ToList();
            }
        }

        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        public BusinessType Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {
                return db.QuerySingle<BusinessType>(My.Table_BusinessType.SelectSingle, new { businessTypeId=id });
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]BusinessType value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_BusinessType.SelectSingle}) {My.Table_BusinessType.Update} ELSE {My.Table_BusinessType.Insert}", value);
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
                    int result = db.Execute(My.Table_BusinessType.Delete, new { businessTypeId = id });
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