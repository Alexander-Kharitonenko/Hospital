using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Models.ModelForRegistrationCard
{
    public class ViewModelAllCard
    {
         public IEnumerable<ViewModelBaseTable> AllCard { get; set; }
         public string Filter { get; set; }
    }
}
