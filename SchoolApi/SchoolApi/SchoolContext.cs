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
        public DbSet<MotionDetector> MotionDetector { get; set; }
        public DbSet<PhotoResistor> PhotoResistor { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HumidityTempSensor>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<HumidityTempSensor>()
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<PhotoResistor>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<PhotoResistor>()
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<MotionDetector>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<MotionDetector>()
                .Property(x => x.Id).IsRequired()
                .ValueGeneratedOnAdd();


            //modelBuilder.Entity("TempDt")
            //    .Property("TimeOccured")
            //    .HasColumnType("DateTime");

            modelBuilder.Entity("MotionCode")
                .Property("motionCodeId").HasColumnType("int").IsRequired();
            modelBuilder.Entity("MotionCode")
                .HasKey("motionCodeId");

            modelBuilder.Entity("MotionCode")
                .Property("motionName").HasColumnType("nvarchar(50)").IsRequired();

            modelBuilder.Entity("MotionCode")
                .HasOne("MotionDetector", "MotionCode")
                .WithOne("motionCodeId")
                .HasForeignKey("MotionDetector", "MotionCode");
        }
    }
}
