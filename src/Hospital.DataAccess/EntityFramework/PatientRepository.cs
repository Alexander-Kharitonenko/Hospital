using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ImplementationRepository
{
    public class PatientRepository : Repository<Patient>
    {
        
        public PatientRepository(HospitalContext contextDb) : base(contextDb) 
        {

        }

    }
}
