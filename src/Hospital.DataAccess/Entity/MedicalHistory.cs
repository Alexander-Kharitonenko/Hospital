using System;

namespace Hospital.DataAccess.Entity
{
    /// <summary>
    /// class describes an entity MedicalHistory
    /// </summary>
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
