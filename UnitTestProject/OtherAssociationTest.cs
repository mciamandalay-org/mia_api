using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class OtherAssociationTest
    {
        [TestMethod]
        public void Get()
        {
            OtherAssociationController otherAssociationController = new OtherAssociationController();
            OtherAssociation[] cities = otherAssociationController.Get().ToArray();

            Assert.IsNotNull(cities);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            OtherAssociationController otherAssociationController = new OtherAssociationController();

            //create new entity
            OtherAssociation otherAssociation = new OtherAssociation();            
            otherAssociation.otherAssociationId = Guid.NewGuid();
            otherAssociation.name = "Test Name";
            otherAssociation.entryDate = DateTime.Now;
            otherAssociation.appUserId = Guid.NewGuid();
            otherAssociation.modifiedDate = DateTime.Now;
            otherAssociation.remark = "Test Remark";

            //insert
            var result1 = otherAssociationController.Post(otherAssociation);
            //update
            var result2 = otherAssociationController.Post(otherAssociation);
            //delete
            var result3 = otherAssociationController.Delete(otherAssociation.otherAssociationId);

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
