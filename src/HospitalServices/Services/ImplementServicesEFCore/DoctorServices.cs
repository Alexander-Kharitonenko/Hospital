using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interface;
using Services.InterfaceServicec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImplementServices
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

        public IEnumerable<Doctor> GedDoctorById(int Id)
        {
           var result =  Context.GetAllEntityBy(el=>el.Id == Id);
            return result;
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
