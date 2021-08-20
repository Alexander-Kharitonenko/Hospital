using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class MedicalHistory : IEntity
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
