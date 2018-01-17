using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIAAPI.Models;
using MIAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void Get()
        {
            PersonController personController = new PersonController();
            Person[] persons = personController.Get().ToArray();

            Assert.IsNotNull(persons);
        }

        [TestMethod]
        public void InsertUpdateDelete()
        {
            PersonController personController= new PersonController();

            //create new entity
            Person person = new Person();
            person.personId = Guid.NewGuid();
            person.name = "Test Name";
            person.dateOfBirth = DateTime.Now;
            person.entryDate = DateTime.Now;
            person.appUserId = Guid.NewGuid();
            person.modifiedDate = DateTime.Now;
            person.remark = "Test Remark";
            person.homeCityId = null;
            person.nativeCityId = null;
            person.homeTownshipId = null;

            //insert
            var result1 = personController.Post(person);
            //update
            var result2 = personController.Post(person);
            //delete
            var result3 = personController.Delete(person.personId);

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
