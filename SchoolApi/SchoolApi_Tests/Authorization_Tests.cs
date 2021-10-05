using Autofac.Extras.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApi.Interfaces;
using Xunit;
using SchoolApi;
using SchoolApi.Controllers;
using SchoolApi.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace SchoolApi_Tests
{
    public class Authorization_Tests
    {
        [Fact]
        public void GetDataEntries_CanJWTAuthorize()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                LoginController cls = mock.Create<LoginController>();
                User user = new User
                {
                    Username = "Hello",
                    Password = "World"
                };

                var result = cls.Login(user);

                OkObjectResult results = (OkObjectResult)result;

                string s = (string)results.Value;

                //cls.GetDataEntries("test");

                mock.Mock<AngularController>().Verify(x => x.GetDataEntries("test"), Times.Once());

            }
        }

        [Fact]
        public void GetDataEntries_CantJWTAuthorize()
        {

        }

    }
}
