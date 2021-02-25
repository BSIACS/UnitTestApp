using UnitTestApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace UnitTestApp.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexViewDataMessage()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            ViewResult result = controller.Index() as ViewResult;
            
            //Assert
            Assert.Equal("Hello, Jason!", result?.ViewData["Message"]);
        }
    }
}
