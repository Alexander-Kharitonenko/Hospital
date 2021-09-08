using Hospital.DataAccess.Interfaces;
using HospitalMVCApplication.Models.ModelForMedicalHistory;
using Microsoft.AspNetCore.Mvc;
using Hospital.Services.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Controllers
{
    public class MedicalHistoryController : Controller
    {
        private readonly IMedicalHistoryServices MedicalHistoryServices;
        public MedicalHistoryController(IMedicalHistoryServices medicalHistoryServices) 
        {
            MedicalHistoryServices = medicalHistoryServices;
        }

        [HttpGet]
        public IActionResult GetAllMedicalHistory()
        {
            ViewModelForMedicalHistory Model = new ViewModelForMedicalHistory() { AllMedicalHistories = MedicalHistoryServices.GetAllMedicalHistory() };
            return View(Model);
        }

        [HttpGet]
        public IActionResult Remove(MedicalHistoryDTO request)
        {
            return View(request);
        }

        //[HttpPost]
        //public async Task<IActionResult> Remove(int id)
        //{
        //    var doctor = DoctorServices.GetAllDoctor().FirstOrDefault(el => el.Id == id);
        //    await DoctorServices.DeleteDoctor(doctor);
        //    return RedirectToAction(nameof(GetAllDoctor));
        //}
    }
}
