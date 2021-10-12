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

namespace SchoolApi.Attributes
{
    //IAuthorizationFilter is ran before the IActionFilter
    //check daily report for link
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        //Runs before hitting the endpoint
        //If any errors occur, set context.Result instead of letting it go through
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                //Get token from incoming request header
                string token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (token == null || token.ToString().Trim() == "")
                {
                    // not logged in
                    context.Result = new JsonResult(new { message = "No token was found" }) { StatusCode = StatusCodes.Status403Forbidden };
                }
                //Di our school context
                SchoolContext schoolContext = context.HttpContext.RequestServices.GetService<SchoolContext>();
                UserManager manager = new UserManager(schoolContext);

                User user = manager.GetDatabaseUserFromTokenString(token);

                if (user == null)
                {
                    context.Result = new JsonResult(new
                    {
                        message = "Forbidden, no connection was found",
                        StatusCode = StatusCodes.Status403Forbidden
                    });
                }
                //Run the end point, since user was found
            }
            catch (Exception e)
            {
                context.Result = new JsonResult(new
                {
                    message = "Internal server error",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = e.Message
                });
            }
        }
    }
}
