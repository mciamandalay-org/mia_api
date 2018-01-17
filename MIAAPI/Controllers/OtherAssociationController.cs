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
    [Route("api/OtherAssociation")]
    public class OtherAssociationController : Controller
    {
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<OtherAssociation> Get()
        {
            using (var db = My.ConnectionFactory())
            {
                return db.Query<OtherAssociation>(My.Table_OtherAssociation.Select).ToList();
            }
        }

        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        public OtherAssociation Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {
                return db.QuerySingle<OtherAssociation>(My.Table_OtherAssociation.SelectSingle, new { otherAssociationId=id });
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]OtherAssociation value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_OtherAssociation.SelectSingle}) {My.Table_OtherAssociation.Update} ELSE {My.Table_OtherAssociation.Insert}", value);
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
                    int result = db.Execute(My.Table_OtherAssociation.Delete, new { otherAssociationId = id });
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