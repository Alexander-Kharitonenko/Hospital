
using Hospital.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.EntityFramework
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly HospitalContext contextDb;
        private readonly IDoctorRepository DoctorRepository;
        private readonly IMedicalHistoryRepository MedicalHistoryRepository;
        private readonly IPatientRepository PatientRepository;
        private readonly IRegistrationCardRepository RegistrationCardRepository;

        public UnitOfWork(IDoctorRepository doctorRepository, IMedicalHistoryRepository medicalHistoryRepository, IPatientRepository patientRepository, IRegistrationCardRepository registrationCardRepository) 
        {
            DoctorRepository = doctorRepository;
            MedicalHistoryRepository = medicalHistoryRepository;
            PatientRepository = patientRepository;
            RegistrationCardRepository = registrationCardRepository;
        }
        public IDoctorRepository doctorRepository { get { return DoctorRepository; } }

        public IMedicalHistoryRepository medicalHistoryRepository { get { return MedicalHistoryRepository; } }

        public IPatientRepository patientRepository { get { return PatientRepository; } }

        public IRegistrationCardRepository registrationCardRepository { get { return RegistrationCardRepository; } }

        public void Dispose()
        {
            contextDb?.Dispose();
            GC.SuppressFinalize(this);

        }
        public async Task<int> SaveChangesAsync()
        {
            return await contextDb.SaveChangesAsync();
        }
    }
}
