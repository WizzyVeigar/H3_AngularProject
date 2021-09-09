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
        public ICollection<PhotoResistor> GetData(string roomNumber)
        {
            throw new NotImplementedException();
        }
    }
}
