using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SchoolApi.Models
{
    public class HumidityTempSensor : DTO
    {
        public float Temperature { get; set; }

        public float Humidity { get; set; }
    }
}
