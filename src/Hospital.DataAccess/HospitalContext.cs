using Hospital.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess
{
    /// <summary>
    /// class for interacting with the database
    /// </summary>
    public class HospitalContext : DbContext
    {
        /// <summary>
        ///constructor for context initialization
        /// </summary>
        /// <param name="options">initializes the context</param>
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {
            
        }

        /// <summary>
        /// property for referencing the table Doctors
        /// </summary>
        public DbSet<Doctor> Doctors { get; set; }

        /// <summary>
        /// property for referencing the table MedicalHistories
        /// </summary>
        public DbSet<MedicalHistory> MedicalHistories { get; set; }

        /// <summary>
        /// property for referencing the table Patients
        /// </summary>
        public DbSet<Patient> Patients { get; set; }

        /// <summary>
        /// property for referencing the table RegistrationСards
        /// </summary>
        public DbSet<RegistrationCard> RegistrationСards { get; set; }
    }
}
