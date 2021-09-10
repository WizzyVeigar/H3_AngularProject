using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Interfaces;
using SchoolApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/Resistor")]
    public class PhotoResistorController : ControllerBase, IFetchData<PhotoResistor>
    {
        public PhotoResistorController(DbContext context)
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
        public List<PhotoResistor> GetData(string roomNumber)
        {
            try
            {
                List<PhotoResistor> resistors = ((SchoolContext)Context).DataEntry
                    .Where(x => x.RoomNumber.ToLower() == roomNumber.ToLower())
                    .Select(x => x.PhotoResistor).ToList();

                return resistors;
            }
            catch (InvalidCastException)
            {
                //log error
                return null;
            }
        }
    }
}
