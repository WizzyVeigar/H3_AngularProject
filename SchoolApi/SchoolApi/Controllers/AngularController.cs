using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        [JwtAuthorize]
        [Route("GetRoom")]
        public List<DataEntry> GetDataEntries(string roomNumber)
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

        /// <summary>
        /// Get the latest recorded value of a single room
        /// </summary>
        /// <param name="roomNumber">The room's number of which entry you want to find</param>
        /// <returns>Returns the latest data entry</returns>
        [HttpGet]
        [Route("LatestSingle")]
        public DataEntry GetLatestEntrySingleRoom(string roomNumber)
        {
            try
            {
                List<DataEntry> collection = ((SchoolContext)Context).DataEntry
                    .Where(x => x.RoomNumber.ToLower() == roomNumber.ToLower()).Include(e => e.HumidityTempSensor).Include(e => e.PhotoResistor).ToList();

                DataEntry entry = null;
                for (int i = 0; i < collection.Count; i++)
                {
                    DataEntry dbData = collection[i];
                    //Give it the first value in the List. Ff there are no elements, we return null in exception
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

        [HttpGet]
        [Route("LatestAll")]
        public List<DataEntry> GetLatestEntryAllRooms()
        {
            try
            {
                SchoolContext con = (SchoolContext)Context;

                //Need to initialize the list for the coming loop
                List<DataEntry> sortedEntries = new List<DataEntry>();

                var efEntries = con.DataEntry
                    .Include(x => x.HumidityTempSensor)
                    .Include(x => x.PhotoResistor)
                    .OrderBy(x => x.RoomNumber)
                    .ThenByDescending(x => x.CreatedTime)
                    .ToList();

                for (int i = 0; i < efEntries.Count; i++)
                {
                    bool exists = false;

                    for (int j = 0; j < sortedEntries.Count; j++)
                    {
                        if (efEntries.ElementAt(i).RoomNumber == sortedEntries[j].RoomNumber)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                        sortedEntries.Add(efEntries.ElementAt(i));
                }

                return sortedEntries;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }
    }
}
