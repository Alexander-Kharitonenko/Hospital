using Hospital.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Models.ModelForRegistrationCard
{
    public class ViewModelBaseTable
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите значение для поля")]
        public Patient Patient { get; set; }

        [Required(ErrorMessage = "Укажите значение для поля")]
        public Doctor Doctor { get; set; }

        [Required(ErrorMessage = "Укажите значение для поля")]
        public MedicalHistory Diagnosis { get; set; }

        [Required(ErrorMessage = "Укажите значение для поля")]
        [DataType(DataType.Date, ErrorMessage = "введите дату в формате {yyyy-MM-0:dd}")]
        [DisplayFormat(DataFormatString = "{yyyy-MM-0:dd}", ApplyFormatInEditMode = true)]
        public string DateAdmission { get; set; }
    }
}
