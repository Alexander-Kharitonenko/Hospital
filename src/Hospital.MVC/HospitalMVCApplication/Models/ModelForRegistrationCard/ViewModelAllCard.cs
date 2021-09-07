using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Models.ModelForRegistrationCard
{
    public class ViewModelAllCard
    {
        public IEnumerable<ViewModelBaseTable> AllCard { get; set; }

        [Required(ErrorMessage = "поле должно быть заполненно")]
        [RegularExpression(@"^(?!\s*$)[-a-zA-Z ]*$", ErrorMessage = "используйте только латинские символы")]
        public string NameFilter { get; set; }
    }
}
