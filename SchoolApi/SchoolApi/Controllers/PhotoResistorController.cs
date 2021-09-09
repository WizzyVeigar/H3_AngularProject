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

        [HttpGet]
        public ICollection<PhotoResistor> GetData(string roomNumber)
        {
            throw new NotImplementedException();
        }
    }
}
