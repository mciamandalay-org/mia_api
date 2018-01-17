using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class LanguageTest
    {
        [TestMethod]
        public void Get()
        {
            LanguageController languageController = new LanguageController();
            Language[] languages = languageController.Get().ToArray();

            Assert.IsNotNull(languages);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            LanguageController languageController = new LanguageController();

            //create new entity
            Language language = new Language();            
            language.languageId = Guid.NewGuid();
            language.name = "Test Name";
            language.entryDate = DateTime.Now;
            language.appUserId = Guid.NewGuid();
            language.modifiedDate = DateTime.Now;
            language.remark = "Test Remark";

            //insert
            var result1 = languageController.Post(language);
            //update
            var result2 = languageController.Post(language);
            //delete
            var result3 = languageController.Delete(language.languageId);

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
