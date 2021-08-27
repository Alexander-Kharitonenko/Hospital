using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;

namespace Hospital.DataAccess.RepositoryEntityFramework
{
    /// <summary>
    /// stores the logic of the repository with the MedicalHistory
    /// </summary>
    public class MedicalHistoryRepository : Repository<MedicalHistory>, IMedicalHistoryRepository
    {
        /// <summary>
        /// constructor for MedicalHistoryRepository
        /// </summary>
        /// <param name="contextDb">context for database</param>
        public MedicalHistoryRepository(HospitalContext contextDb) : base(contextDb)
        {
        }
    }
}
