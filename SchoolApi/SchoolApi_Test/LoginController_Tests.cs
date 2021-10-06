using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Controllers;
using SchoolApi.Models;
using NSubstitute;
using Microsoft.Extensions.Configuration;
using SchoolApi;
using NSubstitute.ReturnsExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using SchoolApi.Managers;

namespace SchoolApi_Test
{
    public class LoginController_Tests
    {
        [Test]
        public void Login_IncorrectLogin()
        {

        }

        //[TestCase("GetInvalidTestUsers")]

        public void Login_CorrectLogin()
        {
        }

        [Test]
        public void IsLoggedIn_SuccessfulRefresh()
        {

        }

        [TestCase("sadpidijdsada")]
        [TestCase("921038frosac0ivjpotmwsiPODJADPq0")]
        [TestCase("")]
        [TestCase("                                ")]
        public void IsLoggedIn_InvalidTokenString()
        {

        }

        [Test]
        public void CreateUser_InvalidUser()
        {
            var config = Substitute.For<IConfiguration>();
            var context = Substitute.For<SchoolContext>();
            User testUser = new User
            {
                Username = "",
                Password = "  "
            };

            config["Jwt:Key"].Returns("abcdefghijklmnopq");
            config["Jwt:Issuer"].Returns("https://localhost_Test:48935");

            context.User.ReturnsNull();


            LoginController controller = new LoginController(context, config);

            IActionResult actual = controller.Login(testUser);

            actual.ShouldBeOfType(typeof(BadRequestObjectResult));
        }

        [Test]
        public void CreateUser_AlreadyTakenUser()
        {
            var config = Substitute.For<IConfiguration>();

            var options = new DbContextOptionsBuilder<SchoolContext>()
            .UseInMemoryDatabase(databaseName: "users")
            .Options;
            User newUser = new User
            {
                Username = "Patrick123",
                Password = "TarzanWasGood"
            };
            User dbUser = new User
            {
                Username = "Patrick123",
                Password = "DwayneTheStone"
            };

            using (var context = new SchoolContext(options))
            {
                context.User.Add(dbUser);

                context.SaveChanges();

                LoginController controller = Substitute.For<LoginController>(context, config);

                config["Jwt:Key"].Returns("abcdefghijklmnopq");
                config["Jwt:Issuer"].Returns("https://localhost_Test:48935");

                controller.UserManager = Substitute.For<UserManager>(context);
                //dbUser = controller.UserManager.GetDatabaseUserFromUsername(newUser.Username);

                IActionResult actual = controller.Login(newUser);

                actual.ShouldBeOfType(typeof(ForbidResult));
            }

        }

        private IEnumerable<User> GetInvalidTestUsers()
        {
            yield return new User { Username = "Benny", Password = "hello" };
            yield return new User { Username = "Kent", Password = "eybdoog" };
        }


    }
}
