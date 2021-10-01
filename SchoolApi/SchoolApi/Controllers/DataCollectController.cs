using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Interfaces;
using SchoolApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
        private DbContext context;
        public DbContext Context
        {
            get
            {
                return context;
            }
            set
            {
                context = value;
            }
        }

        [HttpPost]
        [Route("Save")]
        public string PostTemperature(string temperature, string humidity, string light, string roomNumber)
        {
            try
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
            catch (Exception)
            {
                return null;
            }


            Debug.WriteLine(temperature + " and this " + humidity + " and light: " + light);
            return temperature + " and this " + humidity + " and light: " + light;
        }
    }
}
