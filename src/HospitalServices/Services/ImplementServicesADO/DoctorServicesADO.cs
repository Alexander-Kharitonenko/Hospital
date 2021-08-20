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

        public Task DeleteDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Doctor> GedDoctorById(int Id)
        {
            throw new NotImplementedException();
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
