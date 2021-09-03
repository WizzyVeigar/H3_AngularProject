using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Models
{
    public enum MotionCode
    {
        MotionEnded = 0,
        MotionDetected = 1
    }

    public class MotionDetector : DTO
    {
        public MotionCode MotionCode { get; private set; }
        public DateTime TimeDetected { get; private set; }
        public MotionDetector(MotionCode motionCode, DateTime timeDetected)
        {
            MotionCode = motionCode;
            TimeDetected = timeDetected;
        }

    }
}
