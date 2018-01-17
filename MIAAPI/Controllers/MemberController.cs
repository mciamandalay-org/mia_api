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
    [Route("api/Member")]
    [EnableCors("MyPolicy")]
    public class MemberController : Controller
    {
        [HttpGet]
        public IEnumerable<Member> Get()
        {
            using (var db = My.ConnectionFactory())
            {
                 var members = db.Query<Member, Person, MemberType, Member>($@"{My.Table_Member.Select} 
                LEFT OUTER JOIN dbo.Person ON dbo.Member.personId = dbo.Person.personId 
                LEFT OUTER JOIN dbo.MemberType ON dbo.Member.memberTypeId = dbo.MemberType.memberTypeId", 
                (member, person, memberType) =>
                {
                    person.photo = "";
                    member.person = person;
                    member.memberType = memberType;
                    return member;
                }, splitOn: "personId,memberTypeId").ToList();

                return members;
            }
        }

        [HttpGet("MemberType/{memberTypeId}")]
        public IEnumerable<Member> GetMemberFilterWithMemberType(Guid memberTypeId)
        {
            //using (var db = My.ConnectionFactory())
            //{
            //    return db.Query<Member>(My.Table_Member.Select + " WHERE memberTypeId=@memberTypeId", new { memberTypeId = memberTypeId }).ToList();
            //}
            using (var db = My.ConnectionFactory())
            {
                var members = db.Query<Member, Person, MemberType, Member>($@"{My.Table_Member.Select} 
                LEFT OUTER JOIN dbo.Person ON dbo.Member.personId = dbo.Person.personId 
                LEFT OUTER JOIN dbo.MemberType ON dbo.Member.memberTypeId = dbo.MemberType.memberTypeId
                WHERE dbo.Person.memberTypeId='{memberTypeId}'",
               (member, person, memberType) =>
               {
                   person.photo = "";
                   member.person = person;
                   member.memberType = memberType;
                   return member;
               }, splitOn: "personId,memberTypeId").ToList();

                return members;
            }

        }

        [HttpGet("{id}")]
        public Member Get(Guid id)
        {
            using (var db = My.ConnectionFactory())
            {
                Member member = db.QuerySingle<Member>(My.Table_Member.SelectSingle, new { memberId = id });

                if (member.personId != null)
                {
                    PersonController personController = new PersonController();
                    member.person = personController.Get(member.personId);
                    personController.Dispose();
                }
                
                if (member.memberTypeId != null)
                {
                    MemberTypeController memberTypeController = new MemberTypeController();
                    member.memberType = memberTypeController.Get(member.memberTypeId);
                }

                return member;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Member value)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute($@"IF EXISTS({My.Table_Member.SelectSingle}) {My.Table_Member.Update} ELSE {My.Table_Member.Insert}", value);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = My.ConnectionFactory())
                {
                    int result = db.Execute(My.Table_Member.Delete, new { memberId = id });
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