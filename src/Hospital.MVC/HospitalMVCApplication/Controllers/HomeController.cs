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
using Hospital.DataAccess.Entity;

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
        public IActionResult GetAllCard()
        {
         
            ViewModelAllCard allCard = new ViewModelAllCard();
            var baseTable = new List<ViewModelBaseTable>();

            var allCardInDataBase = CardServices.registrationCardRepository.Get();
            foreach (var element in allCardInDataBase) 
            {
                var doctor = CardServices.doctorRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                var patient = CardServices.patientRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                var diagnosis = CardServices.medicalHistoryRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                ViewModelBaseTable baseTabse = new ViewModelBaseTable()
                {
                    Id = element.Id,
                    Doctor = doctor,
                    Patient = patient,
                    DateAdmission = element.DateAdmission.ToShortDateString(),
                    Diagnosis = diagnosis
                };
                baseTable.Add(baseTabse);
            }
            allCard.AllCard = baseTable;
            return View(allCard);
        }

        [HttpPost]
        public IActionResult GerCardByFilter(string filter) 
        {
           return RedirectToAction(nameof(CardByFilter), "Home", new { filter = filter });
        }


        [HttpGet]
        public IActionResult CardByFilter(string filter) 
        {
            

            if (string.Equals(filter, "Doctor", StringComparison.OrdinalIgnoreCase))
            {
                var model = new ViewModelForFilter<Doctor>();
                var AllDoctor = new List<Doctor>();
                var allCardInDataBase = CardServices.registrationCardRepository.Get();
                foreach (var element in allCardInDataBase)
                {
                    var doctor = CardServices.doctorRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                    AllDoctor.Add(doctor);
                }
                model.Filter = AllDoctor;
                model.NameTable = filter;
                return View(model);
            }
            else if(string.Equals(filter, "Patient", StringComparison.OrdinalIgnoreCase)) 
            {
                var model = new ViewModelForFilter<Patient>();
                var AllPatient = new List<Patient>();
                var allCardInDataBase = CardServices.registrationCardRepository.Get();
                foreach (var element in allCardInDataBase)
                {
                    var patient = CardServices.patientRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                    AllPatient.Add(patient);
                }
                model.Filter = AllPatient;
                model.NameTable = filter;
                return View(model);
            }
            else if (string.Equals(filter, "Diagnosis", StringComparison.OrdinalIgnoreCase))
            {
                var model = new ViewModelForFilter<MedicalHistory>();
                var AllDiagnosis = new List<MedicalHistory>();
                var allCardInDataBase = CardServices.medicalHistoryRepository.Get();
                foreach (var element in allCardInDataBase)
                {
                    var diagnosis = CardServices.medicalHistoryRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                    AllDiagnosis.Add(diagnosis);
                }
                model.Filter = AllDiagnosis;
                model.NameTable = filter;
                return View(model);
            }
            return BadRequest();
        }


        [HttpGet]
        public IActionResult Edit(int id) 
        {
                var CardWithDataBase = CardServices.registrationCardRepository.Get().FirstOrDefault(el => el.Id == id); 
                var doctor = CardServices.doctorRepository.Get().FirstOrDefault(el => el.Id == CardWithDataBase.DoctorId);
                var patient = CardServices.patientRepository.Get().FirstOrDefault(el => el.Id == CardWithDataBase.PatientId);
                var diagnosis = CardServices.medicalHistoryRepository.Get().FirstOrDefault(el => el.Id == CardWithDataBase.DiagnosisId);
                ViewModelBaseTable baseTabse = new ViewModelBaseTable()
                {
                    Id = CardWithDataBase.Id,
                    Doctor = doctor,
                    Patient =patient,
                    DateAdmission = CardWithDataBase.DateAdmission.ToString(),
                    Diagnosis = diagnosis
                };

            return View(baseTabse);
        }

        [HttpPost]
        public IActionResult Edit(ViewModelBaseTable reqoest)
        {

            return View();
        }
    }
       
}
