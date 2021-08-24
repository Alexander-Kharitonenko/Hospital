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
        /// <summary>
        /// constructor for DoctorRepository
        /// </summary>
        /// <param name="contextDb">context for database</param>
        public DoctorRepository(HospitalContext contextDb) : base(contextDb) 
        {
        }
    }
}
