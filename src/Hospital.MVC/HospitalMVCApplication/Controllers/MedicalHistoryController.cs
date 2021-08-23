using Hospital.DataAccess.EntityFramework;
using HospitalMVCApplication.Models.ModelForMedicalHistory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Controllers
{
    public class MedicalHistoryController : Controller
    {
        private readonly IUnitOfWork MedicalHistoryServices;
        public MedicalHistoryController(IUnitOfWork medicalHistoryServices) 
        {
            MedicalHistoryServices = medicalHistoryServices;
        }
        [HttpGet]
        public IActionResult GetAllMedicalHistory()
        {
            ViewModelForMedicalHistory Model = new ViewModelForMedicalHistory() { AllMedicalHistories = MedicalHistoryServices.medicalHistoryRepository.Get() };
            return View(Model);
        }
    }
}
