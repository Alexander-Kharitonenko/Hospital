using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {
            
        }

        

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<MedicalHistory> MedicalHistories { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<RegistrationCard> RegistrationCards { get; set; }
    }
}
