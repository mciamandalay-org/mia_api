using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class MemberTypeTest
    {
        [TestMethod]
        public void Get()
        {
            MemberTypeController memberTypeController = new MemberTypeController();
            MemberType[] memberTypes = memberTypeController.Get().ToArray();

            Assert.IsNotNull(memberTypes);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            MemberTypeController memberTypeController = new MemberTypeController();

            //create new entity
            MemberType memberType = new MemberType();            
            memberType.memberTypeId = Guid.NewGuid();
            memberType.name = "Test Name";
            memberType.entryDate = DateTime.Now;
            memberType.appUserId = Guid.NewGuid();
            memberType.modifiedDate = DateTime.Now;
            memberType.remark = "Test Remark";

            //insert
            var result1 = memberTypeController.Post(memberType);
            //update
            var result2 = memberTypeController.Post(memberType);
            //delete
            var result3 = memberTypeController.Delete(memberType.memberTypeId);

            //assert
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);            
            Assert.IsTrue(result1 is OkResult);
            Assert.IsTrue(result2 is OkResult);
            Assert.IsTrue(result3 is OkResult);

        }
    }
}
