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
    [Route("api/PersonLanguage")]
    [EnableCors("MyPolicy")]
    public class PersonLanguageController : Controller
    {
        [HttpGet("{personId}")]
        public IEnumerable<PersonLanguage> Get(string personId)
        {
            using (var db = My.ConnectionFactory())
            {
                var personLanguages = db.Query<PersonLanguage, Language, PersonLanguage>($@"{My.Table_PersonLanguage.Select} 
            INNER JOIN dbo.Language ON dbo.PersonLanguage.languageId = dbo.Language.languageId
            WHERE (dbo.PersonLanguage.personId = '{personId}')", (personLanguage, language) =>
                {
                    personLanguage.language = language;
                    return personLanguage;
                }, splitOn: "languageId").ToList();

                return personLanguages;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]PersonLanguage value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_PersonLanguage.SelectSingle}) {My.Table_PersonLanguage.Update} ELSE {My.Table_PersonLanguage.Insert}", value);
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
                    int result = db.Execute(My.Table_PersonLanguage.Delete, new { personLanguageId = id });
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