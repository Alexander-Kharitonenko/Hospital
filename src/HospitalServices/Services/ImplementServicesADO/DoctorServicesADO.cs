using DataAccess.Entity;
using Hospital.DataAccess.ADO;
using RepositoryADO.InterfaceForRepository;
using Services.InterfaceServicec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImplementServicesADO
{
    public class DoctorServicesADO  : IDoctorServices
    {
        public readonly DoctorRepositoryADO Context;
        public DoctorServicesADO(DoctorRepositoryADO context) 
        {
            Context = context;
        }

        public async Task Add(Doctor doctor)
        {
            await Context.CreateEntity(doctor);
        }

        public async Task DeleteDoctor(Doctor doctor)
        {
           await Context.Delete(doctor);
        }

        public IEnumerable<Doctor> GedDoctorById(int Id)
        {
           var result = Context.GetAllEntityBy(el => el.Id == Id);
           return result;
        }

        public IEnumerable<Doctor> GetAllDoctor()
        {
            throw new NotImplementedException();
        }

        public Task UpdateDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }
    }
}
