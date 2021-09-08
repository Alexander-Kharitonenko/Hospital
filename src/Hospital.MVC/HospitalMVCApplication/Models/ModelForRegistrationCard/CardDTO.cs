using Hospital.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Models.ModelForRegistrationCard
{
    public class CardDTO
    {
        public int Id { get; set; }

        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; }

        public MedicalHistory Diagnosis { get; set; }

        public string DateAdmission { get; set; }
    }
}
