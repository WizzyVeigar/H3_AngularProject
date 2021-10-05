using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Interfaces
{
    //Used so we can inject all our controllers with SchoolContext or change it if needed
    public interface IHaveDbContext
    {
        public DbContext Context { get; set; }
    }
}
