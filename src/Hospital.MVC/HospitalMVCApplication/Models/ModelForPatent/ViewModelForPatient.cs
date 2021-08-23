using Hospital.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Models.ModelForPatent
{
    public class ViewModelForPatient
    {
        public IEnumerable<Patient> AllPatient;
    }
}
