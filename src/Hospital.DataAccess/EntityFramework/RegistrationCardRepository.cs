using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.EntityFramework
{
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
