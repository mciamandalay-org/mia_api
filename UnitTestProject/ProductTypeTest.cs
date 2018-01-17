using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class ProductTypeTest
    {
        [TestMethod]
        public void Get()
        {
            ProductTypeController productTypeController = new ProductTypeController();
            ProductType[] cities = productTypeController.Get().ToArray();

            Assert.IsNotNull(cities);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            ProductTypeController productTypeController = new ProductTypeController();

            //create new entity
            ProductType productType = new ProductType();            
            productType.productTypeId = Guid.NewGuid();
            productType.name = "Test Name";
            productType.entryDate = DateTime.Now;
            productType.appUserId = Guid.NewGuid();
            productType.modifiedDate = DateTime.Now;
            productType.remark = "Test Remark";

            //insert
            var result1 = productTypeController.Post(productType);
            //update
            var result2 = productTypeController.Post(productType);
            //delete
            var result3 = productTypeController.Delete(productType.productTypeId);

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
