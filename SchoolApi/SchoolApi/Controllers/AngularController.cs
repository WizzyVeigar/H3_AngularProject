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

        
        /// <summary>
        /// Gets all entries of a specific room, or if not defined, all rooms with all entries
        /// </summary>
        /// <param name="roomNumber">The specified room's number</param>
        /// <returns>Returns a list of Entries, if an error occurs; return null</returns>
        [Route("GetRoom")]
        [HttpGet]
        public List<DataEntry> GetDataEntries(string roomNumber)
        {
            using (Context = new SchoolContext())
            {
                try
                {

                    //if room number is null or wasn't specified, get all rooms instead
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

        /// <summary>
        /// Get the latest recorded value of a single room
        /// </summary>
        /// <param name="roomNumber">The room's number of which entry you want to find</param>
        /// <returns>Returns the latest data entry</returns>
        [Route("LatestSingle")]
        [HttpGet]
        public DataEntry GetLatestEntrySingleRoom(string roomNumber)
        {
            using (Context = new SchoolContext())
            {
                try
                {
                    List<DataEntry> collection = ((SchoolContext)Context).DataEntry
                        .Where(x => x.RoomNumber.ToLower() == roomNumber.ToLower()).Include(e=>e.HumidityTempSensor).Include(e=>e.PhotoResistor).ToList();

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

        [Route("LatestAll")]
        [HttpGet]
        public List<DataEntry> GetLatestEntryAllRooms()
        {
            using (Context = new SchoolContext())
            {
                try
                {
                    List<DataEntry> entries = null;
                    var entriestemp = ((SchoolContext) Context).DataEntry;
                    entries = entriestemp.OrderBy(x => x.CreatedTime).ThenBy(x => x.RoomNumber).ToList();

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
