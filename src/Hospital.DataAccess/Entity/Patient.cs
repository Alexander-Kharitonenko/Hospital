using System;

namespace Hospital.DataAccess.Entity
{
    /// <summary>
    /// class describes an entity Patient
    /// </summary>
    public class Patient : IEntity
    {
        /// <summary>
        ///  contains field number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  contains field number first name patient
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///  contains field number last name patient
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///  contains field Patronymic patient
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        ///  contains field gender patient
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        ///  contains field residence address patient
        /// </summary>
        public string ResidenceAddress { get; set; }
    }
}
