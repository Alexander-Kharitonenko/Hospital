using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;
using Hospital.Services.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.ImplementServicesEntityFramework
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IDoctorRepository Context;

        public DoctorServices(IDoctorRepository context) 
        {
            Context = context;
        }

        public async Task Add(Doctor doctor)
        {
            await Context.CreateEntity(doctor);
            await  Context.SaveChanges();
        }

        public async Task DeleteDoctor(Doctor doctor)
        {
            await Context.Delete(doctor);
            await Context.SaveChanges();
        }

        public IEnumerable<Doctor> GetAllDoctor()
        {
           var result = Context.Get();
           return result;
        }

        public async Task UpdateDoctor(Doctor doctor)
        {
            await Context.Update(doctor);
            await Context.SaveChanges();
        }
    }
}
