using Microsoft.AspNetCore.Mvc;
using SchoolApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/Arduino")]
    public class DataCollectController : ControllerBase
    {
        [HttpPost]
        [Route("Save")]
        public string PostTemperature(string temperature, string humidity, string light)
        {
            //return temp.TestString + " " + temp.TestInt;
            Debug.WriteLine(temperature + " and this " + humidity + " and light: " + light);
            return temperature + " and this " + humidity + " and light: " + light;
        }
    }
}
