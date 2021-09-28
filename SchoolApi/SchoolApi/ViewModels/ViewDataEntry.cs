using SchoolApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.ViewModels
{
    public class ViewDataEntry
    {

        public string RoomNumber { get; set; }
        public DateTime CreatedTime { get; set; }

        public HumidityTempSensor HumidityTempSensor { get; set; }
        public PhotoResistor PhotoResistor { get; set; }
        public ViewDataEntry(string roomNumber, DateTime createdTime, HumidityTempSensor humidityTempSensor, PhotoResistor photoResistor)
        {
            RoomNumber = roomNumber;
            CreatedTime = createdTime;
            HumidityTempSensor = humidityTempSensor;
            PhotoResistor = photoResistor;
        }
    }
}
