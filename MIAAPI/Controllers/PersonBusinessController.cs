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
    [Route("api/PersonBusiness")]
    [EnableCors("MyPolicy")]
    public class PersonBusinessController : Controller
    {
        [HttpGet("{personId}")]
        public IEnumerable<PersonBusiness> Get(string personId)
        {
            using (var db = My.ConnectionFactory())
            {
                var personBusinesses = db.Query<PersonBusiness>($@"{My.Table_PersonBusiness.Select} WHERE (dbo.PersonBusiness.personId = '{personId}')").ToArray();
                BusinessController businessController = new BusinessController();
                PersonBusinessProductTypeController personBusinessProductTypeController = new PersonBusinessProductTypeController();
                foreach (var pb in personBusinesses)
                {
                    pb.business = businessController.Get(pb.businessId.ToString());
                    pb.personBusinessProductType = personBusinessProductTypeController.Get(pb.personBusinessId.ToString()).ToArray();
                }

                businessController.Dispose();
                personBusinessProductTypeController.Dispose();

                return personBusinesses;
            }            
        }

        [HttpPost]
        public IActionResult Post([FromBody]PersonBusiness value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_PersonBusiness.SelectSingle}) {My.Table_PersonBusiness.Update} ELSE {My.Table_PersonBusiness.Insert}", value);
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
                    int result = db.Execute(My.Table_PersonBusiness.Delete, new { personBusinessId = id });
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