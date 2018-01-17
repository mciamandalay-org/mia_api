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
    [Route("api/Committee")]
    public class CommitteeController : Controller
    {
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<Committee> Get()
        {
            using (var db = My.ConnectionFactory())
            {                
                var committees = db.Query<Committee, FiscalYear, Committee>($@"SELECT * FROM dbo.Committee 
                LEFT OUTER JOIN dbo.FiscalYear AS fiscalYear ON dbo.Committee.fiscalYearId = fiscalYear.fiscalYearId", (committee, fiscalYear) =>
                {
                    committee.fiscalYear = fiscalYear;
                    return committee;
                }, splitOn: "fiscalYearId").ToList();

                return committees;
            }
        }

        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        public IEnumerable<Committee> Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {                
                var committees = db.Query<Committee, FiscalYear, Committee>($@"SELECT * FROM dbo.Committee 
                LEFT OUTER JOIN dbo.FiscalYear AS fiscalYear ON dbo.Committee.fiscalYearId = fiscalYear.fiscalYearId WHERE personId='{id}'", (committee, fiscalYear) =>
                {
                    committee.fiscalYear = fiscalYear;
                    return committee;
                }, splitOn: "fiscalYearId").ToList();

                return committees;
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]Committee value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_Committee.SelectSingle}) {My.Table_Committee.Update} ELSE {My.Table_Committee.Insert}", value);
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
                    int result = db.Execute(My.Table_Committee.Delete, new { committeeId = id });
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