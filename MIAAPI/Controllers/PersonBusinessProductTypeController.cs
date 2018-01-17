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
    [Route("api/PersonBusinessProductType")]
    [EnableCors("MyPolicy")]
    public class PersonBusinessProductTypeController : Controller
    {
        [HttpGet("{personBusinessId}")]
        public IEnumerable<PersonBusinessProductType> Get(string personBusinessId)
        {
            using (var db = My.ConnectionFactory())
            {
                var personBusinessProductTypes = db.Query<PersonBusinessProductType, ProductType, PersonBusinessProductType>($@"{My.Table_PersonBusinessProductType.Select} 
            INNER JOIN dbo.ProductType ON dbo.PersonBusinessProductType.productTypeId = dbo.ProductType.productTypeId
            WHERE (dbo.PersonBusinessProductType.personBusinessId = '{personBusinessId}')", (personBusinessProductType, productType) =>
                {
                    personBusinessProductType.productType = productType;
                    return personBusinessProductType;
                }, splitOn: "productTypeId").ToList();

                return personBusinessProductTypes;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]PersonBusinessProductType value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_PersonBusinessProductType.SelectSingle}) {My.Table_PersonBusinessProductType.Update} ELSE {My.Table_PersonBusinessProductType.Insert}", value);
                }
                return Ok();
            }
            catch (Exception ex)
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
                    int result = db.Execute(My.Table_PersonBusinessProductType.Delete, new { personBusinessProductTypeId = id });
                    if (result > 0) return Ok(); else return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}