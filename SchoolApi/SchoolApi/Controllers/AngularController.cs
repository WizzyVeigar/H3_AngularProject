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
    //Controller for sending objects to the front end
    public class AngularController : Controller, IHaveDbContext
    {
        //DI a DbContext class into the controller
        public AngularController(DbContext context)
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

        [Route("GetRoom")]
        [HttpGet]
        public List<DataEntry> GetDataEntries(string roomNumber)
        {
            using (Context = new SchoolContext())
            {
                try
                {

                    //if room number is null or wasn't specified, get all rooms
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

        [Route("LatestSingle")]
        [HttpGet]
        public DataEntry GetLatestEntrySingleRoom(string roomNumber)
        {
            using (Context = new SchoolContext())
            {
                try
                {
                    List<DataEntry> collection = ((SchoolContext)Context).DataEntry
                        .Where(x => x.RoomNumber.ToLower() == roomNumber.ToLower()).ToList();

                    DataEntry entry = null;
                    for (int i = 0; i < collection.Count; i++)
                    {
                        DataEntry dbData = collection[i];
                        //Give it the first value in the List, if there are no elements, we return null in exception
                        if (i == 0)
                            entry = dbData;

                        if (entry.CreatedTime < dbData.CreatedTime)
                        {
                            entry = dbData;
                        }
                    }
                    return entry;

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
