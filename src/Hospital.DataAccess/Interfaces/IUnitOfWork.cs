using System.Threading.Tasks;

namespace Hospital.DataAccess.Interfaces
{
    /// <summary>
    /// interface for implementation UnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        ///property for doctor repository
        /// </summary>
        IDoctorRepository doctorRepository { get; }

        /// <summary>
        /// property for medicalHistory repository
        /// </summary>
        IMedicalHistoryRepository medicalHistoryRepository { get; }

        /// <summary>
        /// property for patient repository
        /// </summary>
        IPatientRepository patientRepository { get; }

        /// <summary>
        /// property for registrationCard repository
        /// </summary>
        IRegistrationCardRepository registrationCardRepository { get; }

        /// <summary>
        /// method for saving changes to the database
        /// </summary>
        /// <returns>number of changes</returns>
        public Task<int> SaveChangesAsync();

        /// <summary>
        /// method to release unmanaged resources
        /// </summary>
        public void Dispose();
    }
}
