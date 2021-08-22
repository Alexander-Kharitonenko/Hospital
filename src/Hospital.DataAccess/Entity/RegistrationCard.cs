using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.Entity
{
    public class RegistrationCard : IEntity
    {
        /// <summary>
        ///  contains field number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// contains field number Patient
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// contains field number Doctor
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// contains field number Diagnosis
        /// </summary>
        public int DiagnosisId { get; set;}

        /// <summary>
        /// contains field number date admission
        /// </summary>
        public DateTime DateAdmission { get; set; }
    }
}
