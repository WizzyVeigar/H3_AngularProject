using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Models
{
    public class PhotoResistor : DTO
    {
        public int LightLevel { get; set; }
    }
}
