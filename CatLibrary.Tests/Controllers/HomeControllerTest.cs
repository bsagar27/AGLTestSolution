using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatLibrary;
using CatLibrary.Controllers;
using CatLibrary.Models;

namespace CatLibrary.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void TestIndexController()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void TestModelObjectCount()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var owners = result.ViewData.ToList();
            var totalModels = owners.Count();

            // Assert
            Assert.AreEqual(2, totalModels);
        }

        [TestMethod]
        public void TestMaleModelObject()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var owners = result.ViewData.ToList();
            var maleOwners = owners[0];
            var catList = (List < SelectListItem > )maleOwners.Value;           

            // Assert
            Assert.AreEqual(4, catList.Count());
        }

        [TestMethod]
        public void TestFemaleModelObject()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var owners = result.ViewData.ToList();
            var femaleOwners = owners[1];
            var catList = (List<SelectListItem>)femaleOwners.Value;

            // Assert
            Assert.AreEqual(3, catList.Count());
        }


    }
}
