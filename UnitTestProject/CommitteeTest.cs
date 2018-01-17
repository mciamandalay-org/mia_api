using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class CommitteeTest
    {
        [TestMethod]
        public void Get()
        {
            CommitteeController committeeController = new CommitteeController();
            Committee[] cities = committeeController.Get().ToArray();

            Assert.IsNotNull(cities);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            CommitteeController committeeController = new CommitteeController();

            //create new entity
            Committee committee = new Committee();            
            committee.committeeId = Guid.NewGuid();
            committee.fiscalYearId = Guid.NewGuid();
            committee.name = "Test Name";
            committee.objective = "Test Objective";
            committee.entryDate = DateTime.Now;
            committee.appUserId = Guid.NewGuid();
            committee.modifiedDate = DateTime.Now;
            committee.remark = "Test Remark";

            //insert
            var result1 = committeeController.Post(committee);
            //update
            var result2 = committeeController.Post(committee);
            //delete
            var result3 = committeeController.Delete(committee.committeeId);

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
