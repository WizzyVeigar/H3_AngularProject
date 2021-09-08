using Microsoft.EntityFrameworkCore;
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
                .Property<int>(x=>x.Id)
                .HasColumnName("HumId")
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<HumidityTempSensor>()
                .HasKey("HumId");


            modelBuilder.Entity<PhotoResistor>()
                .Property<int>(x=>x.Id)
                .HasColumnName("PhotoResistorId")
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<PhotoResistor>()
                .HasKey("PhotoResistorId");



            modelBuilder.Entity<DataEntry>()
                .Property(x => x.RoomNumber);
            modelBuilder.Entity<DataEntry>()
                .HasKey("RoomNumber", "CreatedTime");


            modelBuilder.Entity<DataEntry>()
                .Property<int>("HumId");


            modelBuilder.Entity<HumidityTempSensor>()
                .HasOne<DataEntry>()
                .WithMany()
                .HasPrincipalKey("HumId")
                .HasForeignKey("HumId");

            modelBuilder.Entity<DataEntry>()
                .Property<int>("PhotoResistorId");

            modelBuilder.Entity<PhotoResistor>()
                .HasOne<DataEntry>()
                .WithMany()
                .HasPrincipalKey("PhotoResistorId")
                .HasForeignKey("PhotoResistorId");

            base.OnModelCreating(modelBuilder);
        }
    }
}
