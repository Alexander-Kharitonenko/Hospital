using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Models.ModelForRegistrationCard
{
    public class RegistrationCardDTO
    { 
        public int Id { get; set; }

        public int PatientId { get; set; }
        
        public int DoctorId { get; set; }

        public int DiagnosisId { get; set; }
       
        public DateTime DateAdmission { get; set; }
    }
}
