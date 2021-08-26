using System;

namespace Hospital.DataAccess.Entity
{   
    /// <summary>
    /// class describes an entity Doctor
    /// </summary>
    public class Doctor : IEntity
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
        public string  LastName { get; set; }

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
