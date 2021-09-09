using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class DataCollectController : ControllerBase, IHaveDbContext
    {
        public DataCollectController(DbContext context)
        {
            Context = context;
        }

        public DbContext Context
        {
            get
            {
                return Context;
            }

            set
            {
                Context = value;
            }
        }

        [HttpPost]
        [Route("Save")]
        public string PostTemperature(string temperature, string humidity, string light, string roomNumber)
        {
            using (Context = new SchoolContext())
            {
                Context.Add(new DataEntry
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

                Context.SaveChanges();
            }

            Debug.WriteLine(temperature + " and this " + humidity + " and light: " + light);
            return temperature + " and this " + humidity + " and light: " + light;
        }
    }
}
