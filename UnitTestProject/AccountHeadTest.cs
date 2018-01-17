using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class AccountHeadTest
    {
        [TestMethod]
        public void Get()
        {
            AccountHeadController accountHeadController = new AccountHeadController();
            AccountHead[] cities = accountHeadController.Get().ToArray();

            Assert.IsNotNull(cities);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            AccountHeadController accountHeadController = new AccountHeadController();

            //create new entity
            AccountHead accountHead = new AccountHead();            
            accountHead.accountHeadId = Guid.NewGuid();
            accountHead.name = "Test Name";
            accountHead.accountType = 1;
            accountHead.entryDate = DateTime.Now;
            accountHead.appUserId = Guid.NewGuid();
            accountHead.modifiedDate = DateTime.Now;
            accountHead.remark = "Test Remark";

            //insert
            var result1 = accountHeadController.Post(accountHead);
            //update
            var result2 = accountHeadController.Post(accountHead);
            //delete
            var result3 = accountHeadController.Delete(accountHead.accountHeadId);

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
