using Hospital.DataAccess.Interfaces;
using HospitalMVCApplication.Models.ModelForPatent;
using Microsoft.AspNetCore.Mvc;
using Hospital.Services.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientServices PatientServices;
        public PatientController(IPatientServices patientServices) 
        {
            PatientServices = patientServices;
        }

        [HttpGet]
        public IActionResult GetAllPatient()
        {
            ViewModelForPatient Model = new ViewModelForPatient() { AllPatient = PatientServices.GetAllPatient() };
            return View(Model);
        }

        [HttpGet]
        public IActionResult Remove(PatientDTO request)
        {
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var doctor = PatientServices.GetAllPatient().FirstOrDefault(el => el.Id == id);
            await PatientServices.DeletePatient(doctor);
            return RedirectToAction(nameof(GetAllPatient));
        }
    }
}
