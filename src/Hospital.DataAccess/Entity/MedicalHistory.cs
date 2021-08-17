using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class MedicalHistory : IEntity
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; }

    }
}
