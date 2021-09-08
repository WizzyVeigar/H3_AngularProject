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
    [Route("api/[controller]")]
    public class TemperatureController : ControllerBase, IFetchData<HumidityTempSensor>
    {
        [HttpGet]
        public ICollection<HumidityTempSensor> GetData(string roomNumber)
        {
            throw new NotImplementedException();
        }
    }
}
