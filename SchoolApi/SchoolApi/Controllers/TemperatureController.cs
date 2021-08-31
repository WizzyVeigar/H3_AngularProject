using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemperatureController : ControllerBase
    {
        [HttpGet]
        public string GetTemp()
        {
            return "hello";
        }

        [HttpPost]
        [Route("GetTemp")]
        public string PostTemperature([FromBody] string temp)
        {
            //return temp.TestString + " " + temp.TestInt;
            return temp;
        }
    }
}
