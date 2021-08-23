using Hospital.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Models.ModelForMedicalHistory
{
    public class ViewModelForMedicalHistory
    {
        public IEnumerable<MedicalHistory> AllMedicalHistories { get; set; }
    }
}
