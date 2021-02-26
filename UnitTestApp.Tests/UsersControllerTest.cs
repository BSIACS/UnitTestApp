using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestApp.Models;
using Xunit;
using Moq;
using UnitTestApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestApp.Tests
{
    public class UsersControllerTest
    {
        [Fact]
        public void GetUsersReturnsAViewResultWithAListOfUsers() {
            //Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestUsers());
            var controller = new UsersController(mock.Object);

            //Act
            var result = controller.GetUsers();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.Model);
            Assert.Equal(GetTestUsers().Count(), model.Count());
        }

        [Fact]
        public void AddUserReturnsARedirectAndAddUser() {
            //Arrange
            var mock = new Mock<IRepository>();
            var controller = new UsersController(mock.Object);
            var newUser = new User() { Name = "Ben"};

            //Act
            var result = controller.AddUser(newUser);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(newUser);
            Assert.Equal("GetUsers", redirectToActionResult.ActionName);
            mock.Verify(u => u.Create(newUser));
        }

        [Fact]
        public void GetUserReturnNotFoundWhenUserNotFound()
        {
            //Arrange
            int testUserId = 5;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.Get(testUserId)).Returns(null as User);
            UsersController controller = new UsersController(mock.Object);

            //Act
            var result = controller.GetUser(testUserId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetUserReturnBadRequestWhenIdIsNull() {
            //Arrange
            var mock = new Mock<IRepository>();
            UsersController controller = new UsersController(mock.Object);

            //Act
            var result = controller.GetUser(null);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetUserReturnViewResultWithUser(){
            //Arrange
            int testUserId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.Get(testUserId)).Returns(GetTestUser());
            var controller = new UsersController(mock.Object);

            //Act
            var result = controller.GetUser(testUserId);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<User>(viewResult.ViewData.Model);
            Assert.Equal("Tom", model.Name);
            Assert.Equal(35, model.Age);
            Assert.Equal(testUserId, model.Id);
        }

        private User GetTestUser() => new User { Id = 1, Name = "Tom", Age = 35 };

        private IEnumerable<User> GetTestUsers()
        {
            var users = new List<User> {
                new User { Id=1, Name="Tom", Age=35},
                new User { Id=2, Name="Alice", Age=29},
                new User { Id=3, Name="Sam", Age=32},
                new User { Id=4, Name="Kate", Age=30}
            };

            return users;
        }
    }
}
