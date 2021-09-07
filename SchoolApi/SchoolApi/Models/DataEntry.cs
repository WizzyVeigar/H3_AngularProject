using System;
using System.Collections.Generic;
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

        public int RoomNumber { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public List<HumidityTempSensor> HumidityTempSensor { get; set; } = new List<HumidityTempSensor>();
        public List<PhotoResistor> PhotoResistor { get; set; } = new List<PhotoResistor>();

    }
}
