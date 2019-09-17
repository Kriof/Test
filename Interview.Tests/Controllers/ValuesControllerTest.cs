using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interview;
using Interview.Controllers;
using Interview.Models;
using Interview.Scripts.Services;

namespace Interview.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            IEnumerable<string> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            ValuesController controller = new ValuesController();
            
            // Act
            Application result = controller.Get(0);

            // Assert
            Assert.AreEqual(result.Id, "3f2b12b8-2a06-45b4-b057-45949279b4e5");
            Assert.AreEqual(result.Id, 197104);
            Assert.AreEqual(result.Id, "Debit");
            Assert.AreEqual(result.Id, "Payment");
            Assert.AreEqual(result.Id, 58.26);
            Assert.AreEqual(result.Id, "2016-07-01T00:00:00");
            Assert.AreEqual(result.Id, true);
            Assert.AreEqual(result.Id, "2016-07-02T00:00:00");
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            ValuesController controller = new ValuesController();
            var appId = new Random().Next(100000, 1000000);
            Application application = new Application()
            {
                Amount = 10,
                ClearedDate = DateTime.Now,
                IsCleared = true,
                Id = Guid.NewGuid(),
                PostingDate = DateTime.Now.AddDays(1),
                Summary = "testSummary",
                Type = "testType",
                ApplicationId = appId
            };
            // Act
            controller.Post(application);
            ApplicationHelper applicationHelper = new ApplicationHelper();
            var appAppended = applicationHelper.GetApplication(appId).Result;
            // Assert
            Assert.AreEqual(appAppended.Id, appAppended.Id);
        }

        [TestMethod]
        public void Put()
        {
            ValuesController controller = new ValuesController();
            var appId = new Random().Next(100000, 1000000);
            var appGuid = Guid.NewGuid();
            Application application = new Application()
            {
                Amount = 10,
                ClearedDate = DateTime.Now,
                IsCleared = true,
                Id = appGuid,
                PostingDate = DateTime.Now.AddDays(1),
                Summary = "testSummary",
                Type = "testType",
                ApplicationId = appId
            };
            // Act
            controller.Post(application);

            Application applicationUpdated = new Application()
            {
                Amount = 10,
                ClearedDate = DateTime.Now,
                IsCleared = true,
                Id = appGuid,
                PostingDate = DateTime.Now.AddDays(1),
                Summary = "testSummaryUpdated",
                Type = "testType",
                ApplicationId = appId
            };

            controller.Put(applicationUpdated);

            
            ApplicationHelper applicationHelper = new ApplicationHelper();
            var appAppended = applicationHelper.GetApplication(appId).Result;
            // Assert
            Assert.AreEqual(appAppended.Summary, "testSummaryUpdated");
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            ApplicationHelper applicationHelper = new ApplicationHelper();
            var appToRemove = applicationHelper.GetApplication(5).Result;
            var idOfAppToRemove = appToRemove.Id;
            // Act
            controller.Delete(5);

            // Assert
            Assert.AreEqual(applicationHelper.GetApplication(5).Id, idOfAppToRemove);
        }
    }
}
