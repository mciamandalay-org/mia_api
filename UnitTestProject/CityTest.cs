using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class CityTest
    {
        [TestMethod]
        public void Get()
        {
            CityController cityController = new CityController();
            City[] cities = cityController.Get().ToArray();

            Assert.IsNotNull(cities);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            CityController cityController = new CityController();

            //create new entity
            City city = new City();            
            city.cityId = Guid.NewGuid();
            city.name = "Test Name";
            city.entryDate = DateTime.Now;
            city.appUserId = Guid.NewGuid();
            city.modifiedDate = DateTime.Now;
            city.remark = "Test Remark";

            //insert
            var result1 = cityController.Post(city);
            //update
            var result2 = cityController.Post(city);
            //delete
            var result3 = cityController.Delete(city.cityId);

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
