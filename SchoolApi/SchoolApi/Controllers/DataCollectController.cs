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
                context.Add(new DataEntry
                {
                    CreatedTime = DateTime.Now,
                    RoomNumber = roomNumber,
                    PhotoResistor = new PhotoResistor
                    {
                        LightLevel = int.Parse(light),
                    },
                    HumidityTempSensor = new HumidityTempSensor()
                    {
                        Humidity = float.Parse(humidity),
                        Temperature = float.Parse(temperature)
                    }
                });

                context.SaveChanges();
            }

            Debug.WriteLine(temperature + " and this " + humidity + " and light: " + light);
            return temperature + " and this " + humidity + " and light: " + light;
        }
    }
}
