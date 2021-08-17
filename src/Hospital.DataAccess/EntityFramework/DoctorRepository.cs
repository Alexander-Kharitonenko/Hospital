using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ImplementationRepository
{
    public class DoctorRepository : Repository<Doctor>
    { 
        public DoctorRepository(HospitalContext contextDb) : base(contextDb) 
        {
        }
    }
}
