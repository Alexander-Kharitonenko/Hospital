using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ImplementationRepository
{
   public class MedicalHistoryRepository : Repository<MedicalHistory>
   {
        public MedicalHistoryRepository(HospitalContext contextDb) : base(contextDb) 
        { 
        }
    }
}
