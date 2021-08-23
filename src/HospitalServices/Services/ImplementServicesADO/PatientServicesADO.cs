using Hospital.DataAccess.ADO;
using Hospital.DataAccess.Entity;
using RepositoryADO.InterfaceForRepository;
using Services.ImplementServices;
using Services.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImplementServicesADO
{
    public class PatientServicesADO : IPatientServices
    {
        public readonly PatientRepositoryADO Context;
        public PatientServicesADO(PatientRepositoryADO context) 
        {
            Context = context;
        }

        public async Task Add(Patient patient)
        {
            await Context.CreateEntity(patient);
            await Context.SaveChanges();
        }

        public async Task DeletePatient(Patient patient)
        {
           await Context.Delete(patient);
           await Context.SaveChanges();
        }

        public IEnumerable<Patient> GedPatientById(int Id)
        {
           var result = Context.GetAllEntityBy(el => el.Id == Id);
           return result;
        }

        public IEnumerable<Patient> GetAllPatient()
        {
           var result = Context.Get();
           return result;
        }

        public async Task UpdatePatient(Patient patien)
        {
           await  Context.Update(patien);
           await Context.SaveChanges();
        }
    }
}
