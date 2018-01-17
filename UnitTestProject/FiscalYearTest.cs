using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class FiscalYearTest
    {
        [TestMethod]
        public void Get()
        {
            FiscalYearController fiscalYearController = new FiscalYearController();
            FiscalYear[] cities = fiscalYearController.Get().ToArray();
            
            Assert.IsNotNull(cities);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            FiscalYearController fiscalYearController = new FiscalYearController();

            //create new entity
            FiscalYear fiscalYear = new FiscalYear();            
            fiscalYear.fiscalYearId = Guid.NewGuid();
            fiscalYear.fiscalYear = DateTime.Now;
            fiscalYear.startDate = DateTime.Now;
            fiscalYear.endDate = DateTime.Now;
            fiscalYear.entryDate = DateTime.Now;
            fiscalYear.appUserId = Guid.NewGuid();
            fiscalYear.modifiedDate = DateTime.Now;
            fiscalYear.remark = "Test Remark";

            //insert
            var result1 = fiscalYearController.Post(fiscalYear);
            //update
            var result2 = fiscalYearController.Post(fiscalYear);
            //delete
            var result3 = fiscalYearController.Delete(fiscalYear.fiscalYearId);

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
