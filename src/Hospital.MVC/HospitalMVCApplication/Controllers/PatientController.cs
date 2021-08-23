using Hospital.DataAccess.EntityFramework;
using HospitalMVCApplication.Models.ModelForPatent;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Controllers
{
    public class PatientController : Controller
    {
        private readonly IUnitOfWork PatientServices;
        public PatientController(IUnitOfWork patientServices) 
        {
            PatientServices = patientServices;
        }

        [HttpGet]
        public IActionResult GetAllPatient()
        {
            ViewModelForPatient Model = new ViewModelForPatient() { AllPatient = PatientServices.patientRepository.Get() };
            return View(Model);
        }
    }
}
