using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SchoolApi.Models
{
    public abstract class DTO
    {
        [Key]
        public int Id { get; set; }
    }
}
