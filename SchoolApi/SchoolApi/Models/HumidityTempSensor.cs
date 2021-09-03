using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Models
{
    public class HumidityTempSensor : DTO
    {
        public float Temperature { get; private set; }

        public float Humidity { get; private set; }

        public HumidityTempSensor(float temperature, float humidity)
        {
            Temperature = temperature;
            Humidity = humidity;
        }
    }
}
