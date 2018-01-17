using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class ReasonTest
    {
        [TestMethod]
        public void Get()
        {
            ReasonController reasonController = new ReasonController();
            Reason[] cities = reasonController.Get().ToArray();

            Assert.IsNotNull(cities);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            ReasonController reasonController = new ReasonController();

            //create new entity
            Reason reason = new Reason();            
            reason.reasonId = Guid.NewGuid();
            reason.description = "Test Name";
            reason.entryDate = DateTime.Now;
            reason.appUserId = Guid.NewGuid();
            reason.modifiedDate = DateTime.Now;
            reason.remark = "Test Remark";

            //insert
            var result1 = reasonController.Post(reason);
            //update
            var result2 = reasonController.Post(reason);
            //delete
            var result3 = reasonController.Delete(reason.reasonId);

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
