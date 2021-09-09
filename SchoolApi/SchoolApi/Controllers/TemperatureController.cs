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
    [Route("api/[controller]")]
    public class TemperatureController : ControllerBase, IFetchData<HumidityTempSensor>
    {
        public TemperatureController(DbContext context)
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

        [HttpGet]
        public ICollection<HumidityTempSensor> GetData(string roomNumber)
        {
            throw new NotImplementedException();
        }
    }
}
