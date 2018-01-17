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
    [Route("api/ProductType")]
    public class ProductTypeController : Controller
    {
        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<ProductType> Get()
        {
            using (var db = My.ConnectionFactory())
            {
                return db.Query<ProductType>(My.Table_ProductType.Select).ToList();
            }
        }

        [HttpGet("{id}")]
        [EnableCors("MyPolicy")]
        public ProductType Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {
                return db.QuerySingle<ProductType>(My.Table_ProductType.SelectSingle, new { productTypeId=id });
            }
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]ProductType value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_ProductType.SelectSingle}) 
                {My.Table_ProductType.Update} ELSE {My.Table_ProductType.Insert}", value);
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
                    int result = db.Execute(My.Table_ProductType.Delete, new { productTypeId = id });
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