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
        public List<HumidityTempSensor> GetData(string roomNumber)
        {
            try
            {
                List<HumidityTempSensor> tempSensors = ((SchoolContext)Context).DataEntry
                    .Where(x => x.RoomNumber.ToLower() == roomNumber.ToLower())
                    .Select(x => x.HumidityTempSensor).ToList();

                return tempSensors;
            }
            catch (InvalidCastException)
            {
                //Log error
                return null;
            }
        }
    }
}
