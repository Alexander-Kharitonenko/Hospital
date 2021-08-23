using Hospital.DataAccess.EntityFramework;
using HospitalMVCApplication.Models;
using HospitalMVCApplication.Models.ModelForMedicalHistory;
using HospitalMVCApplication.Models.ModelForPatent;
using HospitalMVCApplication.Models.ModelForRegistrationCard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork CardServices;

        public HomeController(IUnitOfWork cardServices)
        {
            CardServices = cardServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewModelForCard Model = new ViewModelForCard() { AllCard = CardServices.registrationCardRepository.Get() };
            return View(Model);
        }

    }
       
}
