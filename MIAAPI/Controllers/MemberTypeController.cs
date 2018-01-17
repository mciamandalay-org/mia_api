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
    [Route("api/MemberType")]
    [EnableCors("MyPolicy")]
    public class MemberTypeController : Controller
    {
        [HttpGet]
        public IEnumerable<MemberType> Get()
        {
            using (var db = My.ConnectionFactory())
            {
                return db.Query<MemberType>(My.Table_MemberType.Select).ToList();
            }
        }

        [HttpGet("{id}")]
        public MemberType Get(Guid? id)
        {
            using (var db = My.ConnectionFactory())
            {
                return db.QuerySingle<MemberType>(My.Table_MemberType.SelectSingle, new { memberTypeId=id });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]MemberType value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_MemberType.SelectSingle}) {My.Table_MemberType.Update} ELSE {My.Table_MemberType.Insert}", value);
                }
                return Ok();
            }
            catch(Exception ex)
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
                    int result = db.Execute(My.Table_MemberType.Delete, new { memberTypeId = id });
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