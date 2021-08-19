using DataAccess.Entity;
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

        public void AddRange(IEnumerable<Doctor> doctors)
        {
            throw new NotImplementedException();
        }

        public void DeleteDoctor()
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

        public void UpdateDoctor()
        {
            throw new NotImplementedException();
        }
    }
}
