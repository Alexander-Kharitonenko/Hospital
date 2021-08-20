using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceServices
{
    public interface IMedicalHistoryServices 
    {
        public Task Add(MedicalHistory medicalHistory);

        public IEnumerable<MedicalHistory> GedMedicalHistoryById(int Id);

        public IEnumerable<MedicalHistory> GetAllMedicalHistory();

        public Task UpdateMedicalHistory(MedicalHistory medicalHistory);

        public Task DeleteMedicalHistory(MedicalHistory medicalHistory);
    }
}
