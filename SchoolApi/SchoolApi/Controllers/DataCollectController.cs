using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Attributes;
using SchoolApi.Interfaces;
using SchoolApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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


        /// <summary>
        /// Saves data to the database
        /// </summary>
        /// <param name="temperature">The current temperature</param>
        /// <param name="humidity">The current humidity</param>
        /// <param name="light">The current light level</param>
        /// <param name="roomNumber">The room where the data was collected from</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        [ApiKeyAuth(Key = "SaveData")]
        public string SaveNewDataEntry(string temperature, string humidity, string light, string roomNumber)
        {
            CultureInfo myCIintl = CultureInfo.CreateSpecificCulture("da-DK");

            double sda = double.Parse(temperature, CultureInfo.GetCultureInfo("da-DK"));

            
            //humidity = string.Format(, "{0:C}", humidity);
            light = string.Format(new CultureInfo("da-DK"), "{0:C}", light);
            //temperature = temperature.Replace(".", ",");
            //humidity = humidity.Replace(".", ",");
            //light = light.Replace(".", ",");
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
            catch (FormatException)
            {
                return null;
            }

            Debug.WriteLine(temperature + " and this " + humidity + " and light: " + light);
            return temperature + " and this " + humidity + " and light: " + light;
        }
    }
}
