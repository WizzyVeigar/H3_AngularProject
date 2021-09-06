using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemperatureController : ControllerBase
    {
        public TemperatureController()
        {
            using (SchoolContext context = new SchoolContext())
            {
                context.PhotoResistor.Add(new PhotoResistor(20));
                //context.MotionDetector.Add(new MotionDetector(MotionCode.MotionDetected, DateTime.Now));
                context.SaveChanges();

                var collection = context.Model.GetEntityTypes();

                foreach (var item in collection)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }


        [HttpGet]
        public IActionResult GetTemp()
        {
            return Ok("hello");
        }

        [HttpPost]
        [Route("GetTemp")]
        public string PostTemperature(string temperature, string humidity, string light)
        {
            //return temp.TestString + " " + temp.TestInt;
            Debug.WriteLine(temperature + " and this " + humidity + " and light: " + light);
            return temperature + " and this " + humidity + " and light: " + light;
        }
    }
}
