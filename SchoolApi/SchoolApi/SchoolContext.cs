﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace SchoolApi
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base()
        {
        }

        public DbSet<HumidityTempSensor> HumidityTempSensor { get; set; }
        public DbSet<PhotoResistor> PhotoResistor { get; set; }
        public DbSet<DataEntry> DataEntry { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (LocalDb)\\MSSQLLocalDB; Initial Catalog = SchoolDash; Integrated Security = true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataEntry>()
                .HasKey(c => new {c.RoomNumber, c.CreatedTime});
        }
    }
}