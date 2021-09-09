using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Interfaces
{
    interface IHaveDbContext
    {
        public DbContext Context { get; set; }
    }
}
