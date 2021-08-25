using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;

namespace Hospital.DataAccess.EntityFramework
{
    /// <summary>
    /// stores the logic of the repository with the Doctor
    /// </summary>
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        /// <summary>
        /// constructor for DoctorRepository
        /// </summary>
        /// <param name="contextDb">context for database</param>
        public DoctorRepository(HospitalContext contextDb) : base(contextDb)
        {
        }
    }
}
