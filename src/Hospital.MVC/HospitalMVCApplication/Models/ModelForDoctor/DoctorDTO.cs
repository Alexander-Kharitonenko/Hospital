using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Models.ModelForDoctor
{
    public class DoctorDTO
    {
        /// <summary>
        ///  contains field number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  contains field number first name doctor
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///  contains field number last name doctor
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///  contains field Patronymic doctor
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        ///  contains field number phone doctor
        /// </summary>
        public string NumberPhone { get; set; }
    }
}
