using AppointmentModel;
using AppointmentModel.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AppointmentApi.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public static void InitializeDatabase()
        {
            using var dbContext = new AppDbContext();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            InsertTestData(dbContext);
        }

        private static void InsertTestData(AppDbContext dbContext)
        {
            var patient1 = new Patient { Login = "Patient1", Password = "Password1", Roles = new List<string> { Role.Patient } };
            var patient2 = new Patient { Login = "Patient2", Password = "Password2", Roles = new List<string> { Role.Patient } };

            var doctor1 = new Doctor { Login = "Doctor1", Password = "Password1", Roles = new List<string> { Role.Doctor } };

            var reason1 = new Reason { LangReasonDictionary = new Dictionary<string, string> { { "PL", "Powod1" }, { "EN", "Reason1" } } };
            var reason2 = new Reason { LangReasonDictionary = new Dictionary<string, string> { { "PL", "Powod2" }, { "EN", "Reason2" } } };

            var appointment1 = new Appointment { Doctor = doctor1, Patient = patient1, Description = "Blabla1" };
            var appointment2 = new Appointment { Doctor = doctor1, Patient = patient2, Description = "Blabla2" };

            var appointmentReason1 = new Appointment2Reason { Appointment = appointment1, Reason = reason1 };
            var appointmentReason2 = new Appointment2Reason { Appointment = appointment2, Reason = reason2 };


            dbContext.AddRange(
                patient1,
                patient2,
                doctor1,
                reason1,
                reason2,
                appointment1,
                appointment2,
                appointmentReason1,
                appointmentReason2);
            dbContext.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={Config.DbPath}", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Login).IsUnique();
                entity.Property(e => e.DateTimeAdd).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Roles).HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            });

            modelBuilder.Entity<Reason>(entity =>
            {
                entity.Property(e => e.LangReasonDictionary).HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v));
            });

            modelBuilder.Entity<Appointment2Reason>(entity =>
            {
                entity.HasKey(e => new { e.AppointmentId, e.ReasonId });
                entity.HasOne(e => e.Appointment)
                    .WithMany(e => e.AppointmentReasons)
                    .HasForeignKey(e => e.AppointmentId);
                entity.HasOne(e => e.Reason)
                    .WithMany(e => e.ReasonAppointments)
                    .HasForeignKey(e => e.ReasonId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
