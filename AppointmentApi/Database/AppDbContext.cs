using AppointmentApi.Tools;
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
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Appointment2Reason> Appointment2Reasons { get; set; }
        public DbSet<Reason> Reasons { get; set; }

        public static void InitializeDatabase()
        {
            using var dbContext = new AppDbContext();

            dbContext.Database.EnsureDeleted();

            if (dbContext.Database.EnsureCreated())
                InsertTestData(dbContext);
        }

        private static void InsertTestData(AppDbContext dbContext)
        {
            var hashGenerator = new HashGeneratorSHA256();

            string pass1 = hashGenerator.GenerateHash("Password1");
            string pass2 = hashGenerator.GenerateHash("Password2");

            //TODO: Remove dummy patients
            var patient1 = new Patient { Login = "Patient1", Password = pass1, Roles = new List<string> { Role.Patient }, FullName = "Jon Snow" };
            var patient2 = new Patient { Login = "Patient2", Password = pass2, Roles = new List<string> { Role.Patient } };

            var doctor1 = new Doctor { Login = "Doctor1", Password = pass1, Roles = new List<string> { Role.Doctor }, FullName = "Robert Bogacki", FirstName = "Robert", LastName = "Bogacki" };
            var doctor2 = new Doctor { Login = "Doctor2", Password = pass1, Roles = new List<string> { Role.Doctor }, FullName = "Donald Trump", FirstName = "Donald", LastName = "Trump" };
            var doctor3 = new Doctor { Login = "Doctor3", Password = pass1, Roles = new List<string> { Role.Doctor }, FullName = "Mona Lisa", FirstName = "Mona", LastName = "Lisa" };

            var reason1 = new Reason { ReasonId = 1, LangReasonDictionary = new Dictionary<string, string> { { "pl", "Kaszel" }, { "en", "Coughing" } } };
            var reason2 = new Reason { ReasonId = 2, LangReasonDictionary = new Dictionary<string, string> { { "pl", "Katar" }, { "en", "Runny nose" } } };
            var reason3 = new Reason { ReasonId = 3, LangReasonDictionary = new Dictionary<string, string> { { "pl", "Ból głowy" }, { "en", "Headache" } } };
            var reason4 = new Reason { ReasonId = 4, LangReasonDictionary = new Dictionary<string, string> { { "pl", "Gorączka" }, { "en", "Fever" } } };
            var reason5 = new Reason { ReasonId = 5, LangReasonDictionary = new Dictionary<string, string> { { "pl", "Ból gardła" }, { "en", "Sore throat" } } };
            var reason6 = new Reason { ReasonId = 6, LangReasonDictionary = new Dictionary<string, string> { { "pl", "Bóle mięśni" }, { "en", "Muscle aches" } } };
            var reason7 = new Reason { ReasonId = 7, LangReasonDictionary = new Dictionary<string, string> { { "pl", "Ochłodzenie ciała" }, { "en", "Cold body" } } };
            var reason8 = new Reason { ReasonId = 8, LangReasonDictionary = new Dictionary<string, string> { { "pl", "Rutynowa kontrola" }, { "en", "Routine inspection" } } };
            var reason9 = new Reason { ReasonId = 9, LangReasonDictionary = new Dictionary<string, string> { { "pl", "Sucha skóra" }, { "en", "Dry skin" } } };
            var reason10 = new Reason { ReasonId = 10, LangReasonDictionary = new Dictionary<string, string> { { "pl", "Złe samopoczucie" }, { "en", "Bad mood" } } };
            var reason11 = new Reason { ReasonId = 11, LangReasonDictionary = new Dictionary<string, string> { { "pl", "Inne" }, { "en", "Other" } } };

            //TODO: Remove dummy appointments
            var appointmentReason1 = new Appointment2Reason { Reason = reason1, ReasonId = 1 };
            var appointmentReason3 = new Appointment2Reason { Reason = reason1, ReasonId = 1 };
            var appointmentReason2 = new Appointment2Reason { Reason = reason2, ReasonId = 2 };

            //TODO: Remove dummy appointments
            var appointment1 = new Appointment { Doctor = doctor1, Patient = patient1, Description = "Blabla1", AppointmentDate = new DateTime(2019, 5, 10), AppointmentId = 1, AppointmentReasons = new List<Appointment2Reason> { appointmentReason1 } };
            var appointment3 = new Appointment { Doctor = doctor1, Patient = patient1, Description = "Blabla3", AppointmentDate = new DateTime(2020, 5, 10), AppointmentId = 2, AppointmentReasons = new List<Appointment2Reason> { appointmentReason3 } };
            var appointment2 = new Appointment { Doctor = doctor1, Patient = patient2, Description = "Blabla2" , AppointmentId = 3, AppointmentDate = new DateTime(2020, 5, 10), AppointmentReasons = new List<Appointment2Reason> { appointmentReason1 } };

            //TODO: Remove dummy appointments
            appointmentReason1.Appointment = appointment1;
            appointment1.AppointmentId = 1;
            appointmentReason3.Appointment = appointment3;
            appointment3.AppointmentId = 3;
            appointmentReason2.Appointment = appointment2;
            appointment2.AppointmentId = 2;

            dbContext.AddRange(
                patient1,
                patient2,
                doctor1,
                doctor2,
                doctor3,
                reason1,
                reason2,
                reason3,
                reason4,
                reason5,
                reason6,
                reason7,
                reason8,
                reason9,
                reason10,
                reason11,
                appointment1,
                appointment2,
                appointmentReason1,
                appointmentReason2,
                appointmentReason3);
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
                entity.Property(e => e.Login).IsRequired();
                entity.Property(e => e.Password).IsRequired();
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
