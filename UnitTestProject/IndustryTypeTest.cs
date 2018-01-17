using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class IndustryTypeTest
    {
        [TestMethod]
        public void Get()
        {
            IndustryTypeController industryTypeController = new IndustryTypeController();
            IndustryType[] cities = industryTypeController.Get().ToArray();

            Assert.IsNotNull(cities);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            IndustryTypeController industryTypeController = new IndustryTypeController();

            //create new entity
            IndustryType industryType = new IndustryType();            
            industryType.industryTypeId = Guid.NewGuid();
            industryType.name = "Test Name";
            industryType.entryDate = DateTime.Now;
            industryType.appUserId = Guid.NewGuid();
            industryType.modifiedDate = DateTime.Now;
            industryType.remark = "Test Remark";

            //insert
            var result1 = industryTypeController.Post(industryType);
            //update
            var result2 = industryTypeController.Post(industryType);
            //delete
            var result3 = industryTypeController.Delete(industryType.industryTypeId);

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
