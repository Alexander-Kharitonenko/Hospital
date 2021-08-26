using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;

namespace Hospital.DataAccess.EntityFramework
{
    /// <summary>
    /// stores the logic of the repository with the RegistrationCard
    /// </summary>
    public class RegistrationCardRepository : Repository<RegistrationCard>, IRegistrationCardRepository
    {
        /// <summary>
        /// constructor for RegistrationCardRepository
        /// </summary>
        /// <param name="contextDb">context for database</param>
        public RegistrationCardRepository(HospitalContext contextDb) : base(contextDb)
        {
        }
    }
}
