using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SchoolApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/Arduino")]
    public class DataCollectController : ControllerBase
    {
        ISqlServerDataAccess sqlServer;
        public DataCollectController(ISqlServerDataAccess dataAccess)
        {
            sqlServer = dataAccess;
        }

        [HttpPost]
        [Route("Save")]
        public IActionResult PostTemperature(string temperature, string humidity, string light, string roomNumber)
        {
            if (((SqlServerDataAccess)sqlServer).SaveData(temperature, humidity, light, roomNumber))
            {
                return Ok(temperature + " and this " + humidity + " and light: " + light);
            }
            else
                return BadRequest("Something went wrong");
        }
    }
}
