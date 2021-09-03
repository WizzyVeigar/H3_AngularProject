using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Models
{
    public abstract class DTO
    {
        public DateTime TimeOccured { get; protected set; }
        public int Id { get; set; }
    }
}
