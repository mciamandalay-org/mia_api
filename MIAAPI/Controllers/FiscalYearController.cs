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
    [Route("api/FiscalYear")]
    public class FiscalYearController : Controller
    {
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<FiscalYear> Get()
        {
            using (var db = My.ConnectionFactory())
            {                
                return db.Query<FiscalYear>(My.Table_FiscalYear.Select).ToList();
            }
        }

        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        public FiscalYear Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {                
                return db.QuerySingle<FiscalYear>(My.Table_FiscalYear.SelectSingle, new { fscalYearId=id });
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]FiscalYear value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_FiscalYear.SelectSingle}) {My.Table_FiscalYear.Update} ELSE {My.Table_FiscalYear.Insert}", value);
                }
                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                    int result = db.Execute(My.Table_FiscalYear.Delete, new { fiscalYearId = id });
                    if (result > 0) return Ok(); else return NotFound();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            
        }
    }
}