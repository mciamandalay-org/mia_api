using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class FacilityTest
    {
        [TestMethod]
        public void Get()
        {
            FacilityController facilityController = new FacilityController();
            Facility[] cities = facilityController.Get().ToArray();

            Assert.IsNotNull(cities);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            FacilityController facilityController = new FacilityController();

            //create new entity
            Facility facility = new Facility();            
            facility.facilityId = Guid.NewGuid();
            facility.name = "Test Name";
            facility.entryDate = DateTime.Now;
            facility.appUserId = Guid.NewGuid();
            facility.modifiedDate = DateTime.Now;
            facility.remark = "Test Remark";

            //insert
            var result1 = facilityController.Post(facility);
            //update
            var result2 = facilityController.Post(facility);
            //delete
            var result3 = facilityController.Delete(facility.facilityId);

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
