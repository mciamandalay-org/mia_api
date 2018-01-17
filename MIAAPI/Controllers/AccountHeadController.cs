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
    [Route("api/AccountHead")]
    public class AccountHeadController : Controller
    {
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<AccountHead> Get()
        {
            using (var db = My.ConnectionFactory())
            {                
                return db.Query<AccountHead>(My.Table_AccountHead.Select).ToList();
            }
        }

        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        public AccountHead Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {                
                return db.QuerySingle<AccountHead>(My.Table_AccountHead.SelectSingle, new { accountHeadId=id });
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]AccountHead value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_AccountHead.SelectSingle}) {My.Table_AccountHead.Update} ELSE {My.Table_AccountHead.Insert}", value);
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
                    int result = db.Execute(My.Table_AccountHead.Delete, new { accountHeadId = id });
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