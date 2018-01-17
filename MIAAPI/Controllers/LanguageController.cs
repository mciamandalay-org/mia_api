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
    [Route("api/Language")]
    public class LanguageController : Controller
    {
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<Language> Get()
        {
            using (var db = My.ConnectionFactory())
            {
                return db.Query<Language>(My.Table_Language.Select).ToList();
            }
        }

        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        public Language Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {
                return db.QuerySingle<Language>(My.Table_Language.SelectSingle, new { languageId=id });
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]Language value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_Language.SelectSingle}) {My.Table_Language.Update} ELSE {My.Table_Language.Insert}", value);
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
                    int result = db.Execute(My.Table_Language.Delete, new { languageId = id });
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