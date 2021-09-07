using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public ICollection<PhotoResistor> GetData(string roomNumber)
        {
            throw new NotImplementedException();
        }
    }
}
