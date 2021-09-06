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
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

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
            if (filter.Contains("Doctor")|| filter.Contains("NumberPhone"))
            {
                var model = new ViewModelForFilter();
                var allDoctor = new List<string>();
                var allCardInDataBase = CardServices.registrationCardRepository.Get();
                foreach (var element in allCardInDataBase)
                {
                    var selestBy = filter.Replace("Doctor", string.Empty);
                    var doctor = CardServices.doctorRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                    switch (selestBy) 
                    {
                        case "FirstName": allDoctor.Add(doctor.FirstName);break;
                        case "LastName": allDoctor.Add(doctor.LastName); break;
                        case "Patronymic": allDoctor.Add(doctor.Patronymic); break;
                        case "NumberPhone": allDoctor.Add(doctor.NumberPhone); break;  
                    }
                    
                }
                model.Filter = allDoctor;
                model.NameTable = filter;
                return View(model);
            }
            else if(filter.Contains("Patient") || filter.Contains("ResidenceAddress") || filter.Contains("Gender")) 
            {
                var model = new ViewModelForFilter();
                var allPatient = new List<string>();
                var allCardInDataBase = CardServices.registrationCardRepository.Get();
                foreach (var element in allCardInDataBase)
                {
                    var selestBy = filter.Replace("Patient", string.Empty);
                    var patient = CardServices.patientRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                    switch (selestBy)
                    {
                        case "FirstName": allPatient.Add(patient.FirstName); break;
                        case "LastName": allPatient.Add(patient.LastName); break;
                        case "Patronymic": allPatient.Add(patient.Patronymic); break;
                        case "Gender": allPatient.Add(patient.Gender); break;
                        case "ResidenceAddress": allPatient.Add(patient.ResidenceAddress); break;
                    }

                }
                model.Filter = allPatient;
                model.NameTable = filter;
                return View(model);
            }
            else if (filter.Contains("Diagnosis"))
            {
                var model = new ViewModelForFilter();
                var allDiagnosis = new List<string>();
                var allCardInDataBase = CardServices.registrationCardRepository.Get();
                foreach (var element in allCardInDataBase)
                {
                    
                    var patient = CardServices.medicalHistoryRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                    switch (filter)
                    {
                        case "Diagnosis": allDiagnosis.Add(patient.Diagnosis); break;             
                    }

                }
                model.Filter = allDiagnosis;
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

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true
                });
           

            return LocalRedirect(returnUrl);
        }
    }
       
}
