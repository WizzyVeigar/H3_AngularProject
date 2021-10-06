using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolApi.Managers;
using SchoolApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi
{
    //IAuthorizationFilter is ran before the IActionFilter
    //https://stackoverflow.com/questions/19249511/difference-between-iactionfilter-and-iauthorizationfilter    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                //Get token from header
                string token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (token == null || token.ToString() == "")
                {
                    // not logged in
                    context.Result = new JsonResult(new { message = "No token was found" }) { StatusCode = StatusCodes.Status403Forbidden };
                }
                //Di our school context
                SchoolContext schoolContext = context.HttpContext.RequestServices.GetService<SchoolContext>();
                using (UserManager manager = new UserManager(schoolContext))
                {
                    User user = manager.GetDatabaseUserFromTokenString(token);

                    if (user != null)
                    {
                        //context.Result = new JsonResult(new
                        //{
                        //    message = "Acknowledged",
                        //    username = user.Username,
                        //    StatusCode = StatusCodes.Status200OK
                        //});
                    }
                }
            }
            catch (Exception e)
            {
                context.Result = new JsonResult(new
                {
                    message = "Forbidden",
                    StatusCode = StatusCodes.Status403Forbidden,
                    ErrorMessage = e.Message
                });
            }
        }
    }
}
