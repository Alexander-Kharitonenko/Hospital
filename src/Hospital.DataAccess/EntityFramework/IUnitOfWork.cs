using DataAccess.Entity;
using Hospital.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.EntityFramework
{
    public interface IUnitOfWork
    {
        IDoctorRepository doctorRepository { get; }

        IMedicalHistoryRepository medicalHistoryRepository { get; }

        IPatientRepository patientRepository { get; }

        IRegistrationCardRepository registrationCardRepository { get; }

        public  Task<int> SaveChangesAsync();

        public void Dispose();
    }
}
