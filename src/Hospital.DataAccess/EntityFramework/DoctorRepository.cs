using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.EntityFramework
{
    public class DoctorRepository : Repository<Doctor> , IDoctorRepository
    { 
        public DoctorRepository(HospitalContext contextDb) : base(contextDb) 
        {
        }
    }
}
