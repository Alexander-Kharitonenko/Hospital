using Hospital.DataAccess.EntityFramework;
using HospitalMVCApplication.Models.ModelForDoctor;
using Microsoft.AspNetCore.Mvc;
using Services.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork DoctorServices; 

        public DoctorController(IUnitOfWork doctorServices) 
        {
            DoctorServices = doctorServices;
        }

        [HttpGet]
        public IActionResult GetAllDoctor()
        {
            ViewModelForDoctor model = new ViewModelForDoctor() { AllDoctors = DoctorServices.doctorRepository.Get() };
            return View(model);
        }
    }
}
