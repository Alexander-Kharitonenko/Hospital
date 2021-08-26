using Hospital.DataAccess.Interfaces;
using System;
using System.Threading.Tasks;

namespace Hospital.DataAccess.EntityFramework
{
    /// <summary>
    /// the class allows you to work with all repositories
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        /// <summary>
        /// repository field ContextDb
        /// </summary>
        private readonly HospitalContext ContextDb;

        /// <summary>
        /// repository field DoctorRepository
        /// </summary>
        private readonly IDoctorRepository DoctorRepository;

        /// <summary>
        /// repository field MedicalHistoryRepository
        /// </summary>
        private readonly IMedicalHistoryRepository MedicalHistoryRepository;

        /// <summary>
        /// repository field PatientRepository
        /// </summary>
        private readonly IPatientRepository PatientRepository;

        /// <summary>
        /// repository field RegistrationCardRepository
        /// </summary>
        private readonly IRegistrationCardRepository RegistrationCardRepository;

        /// <summary>
        /// the constructor initializes the fields
        /// </summary>
        /// <param name="doctorRepository">repository field</param>
        /// <param name="medicalHistoryRepository">repository field</param>
        /// <param name="patientRepository">repository field</param>
        /// <param name="registrationCardRepository">repository field</param>
        /// <param name="contextDb">field context</param>
        public UnitOfWork(IDoctorRepository doctorRepository, IMedicalHistoryRepository medicalHistoryRepository, IPatientRepository patientRepository, IRegistrationCardRepository registrationCardRepository, HospitalContext contextDb)
        {
            DoctorRepository = doctorRepository;
            MedicalHistoryRepository = medicalHistoryRepository;
            PatientRepository = patientRepository;
            RegistrationCardRepository = registrationCardRepository;
            ContextDb = contextDb;
        }

        /// <summary>
        ///property for doctor repository
        /// </summary>
        public IDoctorRepository doctorRepository { get { return DoctorRepository; } }

        /// <summary>
        /// property for medicalHistory repository
        /// </summary>
        public IMedicalHistoryRepository medicalHistoryRepository { get { return MedicalHistoryRepository; } }

        /// <summary>
        /// property for patient repository
        /// </summary>
        public IPatientRepository patientRepository { get { return PatientRepository; } }

        /// <summary>
        /// property for registrationCard repository
        /// </summary>
        public IRegistrationCardRepository registrationCardRepository { get { return RegistrationCardRepository; } }

        /// <summary>
        /// method to release unmanaged resources
        /// </summary>
        public void Dispose()
        {
            ContextDb?.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// method for saving changes to the database
        /// </summary>
        /// <returns>number of changes</returns>
        public async Task<int> SaveChangesAsync()
        {
            var result = await ContextDb.SaveChangesAsync();
            return result;
        }
    }
}
