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
using SchoolApi.Encryption;

namespace SchoolApi_Test
{
    public class LoginController_Tests
    {
        //[Test]
        //public void Login_IncorrectLogin()
        //{

        //}

        ////[TestCase("GetInvalidTestUsers")]

        //public void Login_CorrectLogin()
        //{
        //}

        //[Test]
        //public void IsLoggedIn_SuccessfulRefresh()
        //{

        //}

        [TestCase("sadpidijdsada")]
        [TestCase("921038frosac0ivjpotmwsiPODJADPq0")]
        [TestCase("")]
        [TestCase("                                ")]
        public void IsLoggedIn_InvalidTokenStrings(string tokenString)
        {
            IConfiguration config = Substitute.For<IConfiguration>();
            var options = new DbContextOptionsBuilder<SchoolContext>()
            .UseInMemoryDatabase(databaseName: "IssuedTokens")
            .Options;

            IssuedToken token = new IssuedToken
            {
                Username = "Test",
                ExpiryDate = DateTime.Now,
                TokenString = "IamALongToKEnString123"
            };

            using (var context = new SchoolContext(options))
            {
                context.IssuedToken.Add(token);

                context.SaveChanges();

                LoginController controller = Substitute.For<LoginController>(context, config);

                bool actual = controller.IsLoggedIn(tokenString);

                actual.ShouldBeFalse();
            }
        }

        [Test]
        public void CreateUser_FailOnSpaceOnlyAsUserNameAndPassword()
        {
            IConfiguration config = Substitute.For<IConfiguration>();
            SchoolContext context = Substitute.For<SchoolContext>();
            
            User userToCreate = new User
            {
                Username = "",
                Password = "  "
            };

            config["Jwt:Key"].Returns("abcdefghijklmnopq");
            config["Jwt:Issuer"].Returns("https://localhost_Test:48935");

            context.User.ReturnsNull();

            LoginController controller = new LoginController(context, config);

            IActionResult actual = controller.CreateUser(userToCreate);

            actual.ShouldBeOfType(typeof(BadRequestResult));
        }

        [Test]
        public void CreateUser_AlreadyTakenUser()
        {
            IConfiguration config = Substitute.For<IConfiguration>();

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
                dbUser.Password = Hasher.HashPassword(dbUser.Password);

                context.User.Add(dbUser);

                context.SaveChanges();

                LoginController controller = Substitute.For<LoginController>(context, config);

                config["Jwt:Key"].Returns("abcdefghijklmnopq");
                config["Jwt:Issuer"].Returns("https://localhost_Test:48935");

                controller.UserManager = Substitute.For<UserManager>(context);

                BadRequestResult actual = (BadRequestResult)controller.CreateUser(newUser);

                actual.ShouldBeOfType(typeof(BadRequestResult));
            }
        }
    }
}
