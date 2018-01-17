using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class PositionTest
    {
        [TestMethod]
        public void Get()
        {
            PositionController positionController = new PositionController();
            Position[] cities = positionController.Get().ToArray();

            Assert.IsNotNull(cities);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            PositionController positionController = new PositionController();

            //create new entity
            Position position = new Position();            
            position.positionId = Guid.NewGuid();
            position.committeeId = Guid.NewGuid();
            position.parentPositionId = Guid.NewGuid();
            position.name = "Test Name";
            position.jobDescription = "Test description";
            position.entryDate = DateTime.Now;
            position.appUserId = Guid.NewGuid();
            position.modifiedDate = DateTime.Now;
            position.remark = "Test Remark";

            //insert
            var result1 = positionController.Post(position);
            //update
            var result2 = positionController.Post(position);
            //delete
            var result3 = positionController.Delete(position.positionId);

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
