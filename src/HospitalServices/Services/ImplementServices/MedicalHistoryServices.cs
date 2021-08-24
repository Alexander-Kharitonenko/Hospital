using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;
using Hospital.Services.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.ImplementServices
{
    public class MedicalHistoryServices : IMedicalHistoryServices
    {
        private readonly IMedicalHistoryRepository Context;

        public MedicalHistoryServices(IMedicalHistoryRepository context) 
        {
            Context = context;
        }

        public async Task Add(MedicalHistory medicalHistory)
        {
            await Context.CreateEntity(medicalHistory);
            await Context.SaveChanges();
        }

        public async Task DeleteMedicalHistory(MedicalHistory medicalHistory)
        {
            await Context.Delete(medicalHistory);
            await Context.SaveChanges();
        }

        public IEnumerable<MedicalHistory> GedMedicalHistoryById(int Id)
        {
            var result = Context.GetAllEntityBy(el => el.Id == Id);
            return result;
        }

        public IEnumerable<MedicalHistory> GetAllMedicalHistory()
        {
            var result = Context.Get();
            return result;
        }

        public async Task UpdateMedicalHistory(MedicalHistory medicalHistory)
        {
            await Context.Update(medicalHistory);
            await Context.SaveChanges();
        }
    }
}
