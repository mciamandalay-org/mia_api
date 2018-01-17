using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class TownshipTest
    {
        [TestMethod]
        public void Get()
        {
            TownshipController townshipController = new TownshipController();
            Township[] townships = townshipController.Get().ToArray();

            Assert.IsNotNull(townships);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            TownshipController townshipController = new TownshipController();

            //create new entity
            Township township = new Township();
            township.townshipId = Guid.NewGuid();
            township.name = "Test Name";
            township.entryDate = DateTime.Now;
            township.appUserId = Guid.NewGuid();
            township.modifiedDate = DateTime.Now;
            township.remark = "Test Remark";

            //insert
            var result1 = townshipController.Post(township);
            //update
            var result2 = townshipController.Post(township);
            //delete
            var result3 = townshipController.Delete(township.townshipId);

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
