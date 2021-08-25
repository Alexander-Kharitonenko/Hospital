using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;

namespace Hospital.DataAccess.EntityFramework
{
    /// <summary>
    /// stores the logic of the repository with the Patient
    /// </summary>
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        /// <summary>
        /// constructor for PatientRepository
        /// </summary>
        /// <param name="contextDb">context for database</param>
        public PatientRepository(HospitalContext contextDb) : base(contextDb)
        {

        }

    }
}
