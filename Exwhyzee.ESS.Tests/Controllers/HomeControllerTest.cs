using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Exwhyzee.ESS;
using Exwhyzee.ESS.Controllers;

namespace Exwhyzee.ESS.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
       

        [TestMethod]
        public void About()
        {
            // Arrange
            IskoolsController controller = new IskoolsController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            IskoolsController controller = new IskoolsController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
