using Hospital.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Models.ModelForRegistrationCard
{
    public class ViewModelForFilter
    {
        public string NameTable { get; set; }
        public IEnumerable<string> Filter { get; set; }
       

    }
}
