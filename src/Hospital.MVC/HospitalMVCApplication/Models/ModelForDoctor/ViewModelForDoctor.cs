using Hospital.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Models.ModelForDoctor
{
    public class ViewModelForDoctor
    {
        public IEnumerable<Doctor> AllDoctors { get; set; }
    }
}
