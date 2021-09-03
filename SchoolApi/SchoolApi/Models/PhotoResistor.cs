using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Models
{
    public class PhotoResistor : DTO
    {
        public int LightLevel { get; private set; }
        public PhotoResistor(int light)
        {
            LightLevel = light;
        }
    }
}
