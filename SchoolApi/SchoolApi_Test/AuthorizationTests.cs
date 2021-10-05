using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolApi;
using SchoolApi.Controllers;
using SchoolApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApi_Test
{
    public class AuthorizationTests
    {

    }
    //{
    //    public void GetDataEntries_CanJWTAuthorize()
    //    {
    //        using (AutoMock mock = AutoMock.GetLoose())
    //        {
    //            JwtAuthorizeAttribute cls = mock.Create<JwtAuthorizeAttribute>();
    //            AuthorizationFilterContext filter = mock.Create<AuthorizationFilterContext>();

    //            string token = GetTokenFromTestUser();
    //            filter.HttpContext.Request.Headers.Add("Authorization", token);

    //            cls.OnAuthorization(filter);

    //            mock.Mock<JwtAuthorizeAttribute>().Verify(x => x.OnAuthorization(filter), Times.Once());

    //        }
    //    }

    //    private string GetTokenFromTestUser()
    //    {
    //        using (AutoMock mock = AutoMock.GetLoose())
    //        {
    //            LoginController login = mock.Create<LoginController>();
    //            User user = new User
    //            {
    //                Username = "Hello",
    //                Password = "World"
    //            };

    //            var result = login.Login(user);

    //            OkObjectResult results = (OkObjectResult)result;

    //            string token = (string)results.Value;

    //            Assert.True(true);
    //            return token;
    //        }
    //    }

    //    [Fact]
    //    public void GetDataEntries_CantJWTAuthorize()
    //    {
    //        Assert.True(true);
    //    }
}
