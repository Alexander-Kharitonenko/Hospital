using System;

namespace Hospital.DataAccess.Entity
{
    /// <summary>
    /// interface describes an  base entity
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        ///  id property
        /// </summary>
        public int Id { get; set; }
    }
}
