using Hospital.DataAccess.Interfaces;
using HospitalMVCApplication.Models;
using HospitalMVCApplication.Models.ModelForMedicalHistory;
using HospitalMVCApplication.Models.ModelForPatent;
using HospitalMVCApplication.Models.ModelForRegistrationCard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hospital.Services.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRegistrationCardServices CardServices;
        private readonly IDoctorServices DoctorServices;

        public HomeController(IRegistrationCardServices cardServices, IDoctorServices doctorServices)
        {
            CardServices = cardServices;
            DoctorServices = doctorServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewModelForCard Model = new ViewModelForCard()
            {
                AllCard = CardServices.GetAllRegistrationCard(),
                AllDoctor = DoctorServices.GetAllDoctor()
            };
            return View(Model);
        }

    }
       
}
