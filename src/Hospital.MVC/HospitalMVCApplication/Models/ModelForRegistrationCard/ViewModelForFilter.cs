using Hospital.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Models.ModelForRegistrationCard
{
    public class ViewModelForFilter
    {
        public string NameTable { get; set; }
        public IEnumerable<Doctor> DoctorFilter { get; set; }
        public IEnumerable<Patient> PatientFilter { get; set; }
        public IEnumerable<MedicalHistory> MedicalHistoryFilter { get; set; }
        public IEnumerable<RegistrationCard> RegistrationCardFilter { get; set; }

    }
}
