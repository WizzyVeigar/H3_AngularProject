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
    public class AngularController : Controller, IHaveDbContext
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

        [Route("GetRoom")]
        [HttpGet]
        public List<DataEntry> GetDataEntries(string roomNumber)
        {
            using (Context = new SchoolContext())
            {
                try
                {
                    List<DataEntry> entries;

                    if (roomNumber != null)
                    {
                        return ((SchoolContext)Context).DataEntry
                            .Where(x => x.RoomNumber.ToLower() == roomNumber.ToLower())
                            .Include(x => x.HumidityTempSensor)
                            .Include(x => x.PhotoResistor).ToList();
                    }
                    return ((SchoolContext)Context).DataEntry
                        .Include(x => x.HumidityTempSensor)
                        .Include(x => x.PhotoResistor).ToList();
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
