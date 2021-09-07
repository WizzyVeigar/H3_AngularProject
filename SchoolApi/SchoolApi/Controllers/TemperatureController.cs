﻿using Microsoft.AspNetCore.Mvc;
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
    [Route("api/[controller]")]
    public class TemperatureController : ControllerBase, IFetchData<HumidityTempSensor>
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
        public ICollection<HumidityTempSensor> GetData(string roomNumber)
        {
            throw new NotImplementedException();
        }
    }
}
