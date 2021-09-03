﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetTemp()
        {
            using (SchoolContext context = new SchoolContext())
            {
                context.MotionDetector.Add(new Models.MotionDetector(Models.MotionCode.MotionDetected, DateTime.Now));
                context.SaveChanges();

                var collection = context.Model.GetEntityTypes();

                foreach (var item in collection)
                {
                    Console.WriteLine(item.Name);
                }
            }

            return Ok("hello");
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
