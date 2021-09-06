using Hospital.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Models.ModelForRegistrationCard
{
    public class ViewModelForFilter
    {      
        public IEnumerable<string> Filter { get; set; }
        public string NameFilter { get; set; }
    }
}
