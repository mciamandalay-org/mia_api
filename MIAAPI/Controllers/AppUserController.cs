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
    [Route("api/AppUser")]
    public class AppUserController : Controller
    {
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<AppUser> Get()
        {
            using (var db = My.ConnectionFactory())
            {
                return db.Query<AppUser>(My.Table_AppUser.Select).ToList();
            }
        }

        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        public AppUser Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {
                return db.QuerySingle<AppUser>(My.Table_AppUser.SelectSingle, new { userId=id });
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]AppUser value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_AppUser.SelectSingle}) {My.Table_AppUser.Update} ELSE {My.Table_AppUser.Insert}", value);
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
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute(My.Table_AppUser.Delete, new { userId = id });
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