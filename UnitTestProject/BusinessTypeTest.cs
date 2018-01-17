using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class BusinessTypeTest
    {
        [TestMethod]
        public void Get()
        {
            BusinessTypeController businessTypeController = new BusinessTypeController();
            BusinessType[] cities = businessTypeController.Get().ToArray();

            Assert.IsNotNull(cities);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            BusinessTypeController businessTypeController = new BusinessTypeController();

            //create new entity
            BusinessType businessType = new BusinessType();            
            businessType.businessTypeId = Guid.NewGuid();
            businessType.name = "Test Name";
            businessType.entryDate = DateTime.Now;
            businessType.appUserId = Guid.NewGuid();
            businessType.modifiedDate = DateTime.Now;
            businessType.remark = "Test Remark";

            //insert
            var result1 = businessTypeController.Post(businessType);
            //update
            var result2 = businessTypeController.Post(businessType);
            //delete
            var result3 = businessTypeController.Delete(businessType.businessTypeId);

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
