using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.Entity
{
    public interface IEntity
    {
        /// <summary>
        ///  id property
        /// </summary>
        public int Id { get; set; }
    }
}
