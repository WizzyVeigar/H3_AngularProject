using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SchoolApi.Models
{
    public class IssuedToken
    {
        [ForeignKey("User")]
        public string Username { get; set; }
        
        public string TokenString { get; set; }

        public DateTime ExpiryDate { get; set; }

    }
}
