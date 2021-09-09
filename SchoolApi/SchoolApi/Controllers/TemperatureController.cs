﻿using Microsoft.AspNetCore.Mvc;
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
    [Route("api/[controller]")]
    public class TemperatureController : ControllerBase, IFetchData<HumidityTempSensor>
    {
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

        [HttpGet]
        public ICollection<HumidityTempSensor> GetData(string roomNumber)
        {
            throw new NotImplementedException();
        }
    }
}
