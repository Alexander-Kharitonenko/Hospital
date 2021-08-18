using DataAccess.Entity;
using Hospital.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.EntityFramework
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        
        public PatientRepository(HospitalContext contextDb) : base(contextDb) 
        {

        }

    }
}
