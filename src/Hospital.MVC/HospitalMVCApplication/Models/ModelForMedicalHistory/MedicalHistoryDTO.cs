using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Models.ModelForMedicalHistory
{
    public class MedicalHistoryDTO
    {
        /// <summary>
        ///  contains field number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  contains field Diagnosis Patient
        /// </summary>
        public string Diagnosis { get; set; }
    }
}
