using Microsoft.AspNetCore.Authorization;
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
                //Get the potential keys from appsettings.json
                string apiValue = config.GetValue<string>("ApiKeys:" + Key);

                if (apiValue != potentialApiValue)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                await next();
            }

            context.Result = new UnauthorizedResult();
            return;
        }
    }

}
