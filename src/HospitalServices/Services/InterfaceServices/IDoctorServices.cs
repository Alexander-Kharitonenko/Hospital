using Hospital.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceServicec
{
    public interface IDoctorServices
    {
        public Task Add(Doctor doctor);

        public IEnumerable<Doctor> GedDoctorById(int Id);

        public IEnumerable<Doctor> GetAllDoctor();

        public Task UpdateDoctor(Doctor doctor);

        public Task DeleteDoctor(Doctor doctor);

    }
}
