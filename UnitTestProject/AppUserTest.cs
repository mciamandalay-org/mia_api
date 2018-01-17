using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class AppUserTest
    {
        [TestMethod]
        public void Get()
        {
            AppUserController appUserController = new AppUserController();
            AppUser[] cities = appUserController.Get().ToArray();

            Assert.IsNotNull(cities);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            AppUserController appUserController = new AppUserController();

            //create new entity
            AppUser appUser = new AppUser();            
            appUser.userId = Guid.NewGuid();
            appUser.loginName = "Test Name";
            appUser.password = "Test Password";
            appUser.entryDate = DateTime.Now;
            appUser.appUserId = Guid.NewGuid();
            appUser.modifiedDate = DateTime.Now;
            appUser.remark = "Test Remark";

            //insert
            var result1 = appUserController.Post(appUser);
            //update
            var result2 = appUserController.Post(appUser);
            //delete
            var result3 = appUserController.Delete(appUser.userId);

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
