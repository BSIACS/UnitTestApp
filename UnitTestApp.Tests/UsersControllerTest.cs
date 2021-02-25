using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestApp.Models;
using Xunit;
using Moq;
using UnitTestApp.Controllers;

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

            //Assert

        }

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
