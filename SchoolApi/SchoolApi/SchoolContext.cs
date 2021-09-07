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
            //modelBuilder.Entity<DTO>();

            modelBuilder.Entity<HumidityTempSensor>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<HumidityTempSensor>()
                .Property(x => x.Id)
                .HasColumnName("HumId")
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<PhotoResistor>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<PhotoResistor>()
                .Property(x => x.Id)
                .HasColumnName("PhotoResistorId")
                .IsRequired()
                .ValueGeneratedOnAdd();




            modelBuilder.Entity<DataEntry>()
                .Property(x => x.RoomNumber).IsRequired();
            modelBuilder.Entity<DataEntry>()
                .HasKey("RoomNumber", "CreatedTime");


            modelBuilder.Entity<DataEntry>()
                .Property<int>("HumId").HasColumnType("int");

            modelBuilder.Entity<DataEntry>()
                .HasMany(x => x.HumidityTempSensor)
                .WithOne()
                .HasForeignKey(x => x.Id);

            modelBuilder.Entity<DataEntry>()
                .Property<int>("PhotoResistorId").HasColumnType("int");

            modelBuilder.Entity<DataEntry>()
                .HasMany(x=>x.PhotoResistor)
                .WithOne()
                .HasForeignKey(x=> x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
