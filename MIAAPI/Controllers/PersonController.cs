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
    [Route("api/Person")]
    public class PersonController : Controller
    {
        [HttpGet, EnableCors("MyPolicy")]
        public IEnumerable<Person> Get()
        {
            using (var db = My.ConnectionFactory())
            {
                var persons = db.Query<Person, City, Township, City, Person>($@"{My.Table_Person.Select} 
                LEFT OUTER JOIN dbo.City AS homeCity ON dbo.Person.homeCityId = homeCity.cityId 
                LEFT OUTER JOIN dbo.Township ON dbo.Person.homeTownshipId = dbo.Township.townshipId 
                LEFT OUTER JOIN dbo.City AS nativeCity ON dbo.Person.nativeCityId = nativeCity.cityId", (person, homeCity, homeTownship, nativeCity) => 
                {
                    person.homeCity = homeCity;
                    person.homeTownship = homeTownship;
                    person.nativeCity = nativeCity;
                    person.photo = "";
                    return person;
                }, splitOn: "cityId,townshipId,cityId").ToList();

                return persons;
            }
        }
        
        [HttpGet("{id}"), EnableCors("MyPolicy")]
        public Person Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {

                var persons = db.Query<Person, City, Township, City, Person>($@"SELECT * FROM dbo.Person 
                LEFT OUTER JOIN dbo.City AS homeCity ON dbo.Person.homeCityId = homeCity.cityId 
                LEFT OUTER JOIN dbo.Township ON dbo.Person.homeTownshipId = dbo.Township.townshipId 
                LEFT OUTER JOIN dbo.City AS nativeCity ON dbo.Person.nativeCityId = nativeCity.cityId WHERE personId='{id.ToString()}'", (person, homeCity, homeTownship, nativeCity) =>
                {
                    person.homeCity = homeCity;
                    person.homeTownship = homeTownship;
                    person.nativeCity = nativeCity;
                    return person;
                }, splitOn: "cityId,townshipId,cityId").ToArray();

                if (persons.Length == 1)
                {
                    PersonBusinessController personBusinessController = new PersonBusinessController();
                    PersonLanguageController personLanguageController = new PersonLanguageController();

                    var p = persons[0];
                    p.personBusiness = personBusinessController.Get(p.personId.ToString()).ToArray();
                    p.personLanguage = personLanguageController.Get(p.personId.ToString()).ToArray();

                    personBusinessController.Dispose();
                    personLanguageController.Dispose();

                    return p;
                }
                else
                    return null;                
            }
        }        

        [HttpPost, EnableCors("MyPolicy")]
        public IActionResult Post([FromBody]Person value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_Person.SelectSingle}) {My.Table_Person.Update} ELSE {My.Table_Person.Insert}", value);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}"), EnableCors("MyPolicy")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute(My.Table_Person.Delete, new { personId = id });
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