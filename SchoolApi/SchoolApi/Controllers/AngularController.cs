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
    [Route("Api/Angular")]
    public class AngularController : Controller, IGetFromEF<DataEntry>
    {
        SchoolContext context;

        public DbContext Context
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        [Route("GetRoom")]
        [HttpGet]
        public List<DataEntry> GetDataEntries(string roomNumber)
        {
            using (context = new SchoolContext())
            {
                try
                {
                    var entries = context.DataEntry
                        .Where(x => x.RoomNumber.ToLower() == roomNumber.ToLower())
                        .Include(x => x.HumidityTempSensor)
                        .Include(x => x.PhotoResistor).ToList();
                    return entries;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return null;
                }
            }
        }
    }
}
