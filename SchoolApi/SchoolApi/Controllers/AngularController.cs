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
        public AngularController(DbContext context)
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

        [Route("GetRoom")]
        [HttpGet]
        public List<DataEntry> GetDataEntries(string roomNumber)
        {
            using (Context = new SchoolContext())
            {
                try
                {
                    List<DataEntry> entries;

                    if (roomNumber == null)
                    {
                        entries = ((SchoolContext)Context).DataEntry
                            .Include(x => x.HumidityTempSensor)
                            .Include(x => x.PhotoResistor).ToList();
                    }
                    entries = ((SchoolContext)Context).DataEntry
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
