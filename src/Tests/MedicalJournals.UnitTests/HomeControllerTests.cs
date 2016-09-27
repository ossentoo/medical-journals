using System;
using System.Collections.Generic;
using MedicalJournals.Common.Tests;
using MedicalJournals.Entities.Data;
using MedicalJournals.Models.Data;
using MedicalJournals.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MedicalJournals.UnitTests
{
    [TestClass]
    public class HomeControllerTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly FakeUserManager _userManager;
        private readonly UnitOfWorkMock _uow;

        public HomeControllerTests()
        {
            _userManager = new FakeUserManager();
            _uow = new UnitOfWorkMock();

        }

        [TestMethod]
        public void Index_GetsDefaultJournals()
        {
            // Arrange
            var controller = new HomeController(_uow,_userManager, new TestAppSettings());

            // Action
            var result = (ViewResult)controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsNull(result.ViewName);

            Assert.IsNotNull(result.ViewData);
            Assert.IsNotNull(result.ViewData.Model);

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<Journal>));

            var journals = (List<Journal>) result.ViewData.Model;
            Assert.AreEqual(6, journals.Count);
        }


        [TestMethod]
        public void Request_StatusCodePage_ReturnsStatusCodePage()
        {
            // Arrange
            var controller = new HomeController(_uow, _userManager, new TestAppSettings());
            var statusCodeView = "~/Views/Shared/StatusCodePage.cshtml";

            // Action
            var result = controller.StatusCodePage();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var view = (ViewResult) result;
            Assert.AreEqual(statusCodeView, view.ViewName);
        }
    }
}
