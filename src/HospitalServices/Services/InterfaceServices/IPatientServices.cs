using Hospital.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.InterfaceServices
{
    public interface IPatientServices
    {
        public Task Add(Patient doctor);

        public IEnumerable<Patient> GetAllPatient();

        public Task UpdatePatient(Patient doctor);

        public Task DeletePatient(Patient doctor);

    }
}
