using Microsoft.AspNetCore.Mvc;
using SchoolApi.Interfaces;
using SchoolApi.Models;
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
        SchoolContext context;

        [HttpPost]
        [Route("Save")]
        public string PostTemperature(string temperature, string humidity, string light, string roomNumber)
        {
            using (context = new SchoolContext())
            {
                context.Add<DataEntry>(new DataEntry
                {
                    CreatedTime = DateTime.Now,
                    RoomNumber = roomNumber,
                    PhotoResistor = new PhotoResistor
                    {
                        Id = 1,
                        LightLevel = int.Parse(light),
                    },
                    PhotoResId = 1,
                    HumidityTempSensor = new HumidityTempSensor()
                    {
                        Id = 1,
                        Humidity = float.Parse(humidity),
                        Temperature = float.Parse(temperature)
                    },
                    HumidId = 1
                });

                context.SaveChanges();
            }
            //return temp.TestString + " " + temp.TestInt;
            Debug.WriteLine(temperature + " and this " + humidity + " and light: " + light);
            return temperature + " and this " + humidity + " and light: " + light;
        }
    }
}
