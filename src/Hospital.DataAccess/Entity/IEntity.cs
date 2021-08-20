using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public interface IEntity
    {
        /// <summary>
        ///  id property
        /// </summary>
        public int Id { get; set; }
    }
}
