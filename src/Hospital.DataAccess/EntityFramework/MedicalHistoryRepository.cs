using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.EntityFramework
{
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
