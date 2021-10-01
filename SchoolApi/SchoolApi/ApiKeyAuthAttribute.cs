using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi
{
    //What can we put this attribute on?
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : AuthorizeAttribute, IAsyncActionFilter
    {
        public string Key { get; set; }

        //This is used to authorize the request before hitting the controller
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Headers.TryGetValue(Key, out var potentialApiValue))
            {
                //Dependency inject the config object
                IConfiguration config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                //Get the potential key from appsettings.json
                string apiValue = config.GetValue<string>("ApiKeys:" + Key);

                if (apiValue != potentialApiValue)
                {
                    //The message and statuscode are split, so statuscode doesn't show in the response body
                    context.Result = new JsonResult(new
                    {
                        message = "Forbidden. Api key is invalid"
                    })
                    { StatusCode = StatusCodes.Status403Forbidden };
                    return;
                }
                //Run next if no errors occurred
                await next();
            }

            context.Result = context.Result = new JsonResult(new
            {
                message = "Forbidden. No api key was in the header 'Authorization'"
            })
            { StatusCode = StatusCodes.Status403Forbidden };
            return;
        }
    }

}
