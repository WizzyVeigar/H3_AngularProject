using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Models
{
    public class DataEntry
    {
        public DataEntry()
        {
            CreatedTime = new DateTime();
        }

        public string RoomNumber { get; set; }
        public DateTime CreatedTime { get; set; }

        [ForeignKey("HumidityTempSensor")]
        public int HumidId { get; set; }

        [ForeignKey("PhotoResistor")]
        public int PhotoResId { get; set; }

        public HumidityTempSensor HumidityTempSensor { get; set; }
        public PhotoResistor PhotoResistor { get; set; }

    }
}
