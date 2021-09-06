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
        public IActionResult GetAllCard(ViewModelAllCard request) 
        {
            if (ModelState.IsValid) 
            {
                return RedirectToAction(nameof(CardByFilter), "Home", new { filter = request.NameFilter});
            }

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


        [HttpGet]
        public IActionResult CardByFilter(string filter) 
        {
            if (filter.Contains("Doctor ", StringComparison.OrdinalIgnoreCase) || filter.Contains("Number Phone", StringComparison.OrdinalIgnoreCase))
            {

                var model = new ViewModelForFilter();
                var allDoctor = new List<string>();
                var allCardInDataBase = CardServices.registrationCardRepository.Get();
                foreach (var element in allCardInDataBase)
                {
                    var selestBy = filter.Replace("Doctor ", string.Empty, StringComparison.OrdinalIgnoreCase).Replace(" ", string.Empty);
                    var doctor = CardServices.doctorRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                    switch (true)
                    {
                        case true when string.Equals("FirstName", selestBy, StringComparison.OrdinalIgnoreCase): allDoctor.Add(doctor.FirstName); break;
                        case true when string.Equals("LastName", selestBy, StringComparison.OrdinalIgnoreCase): allDoctor.Add(doctor.LastName); break;
                        case true when string.Equals("Patronymic", selestBy, StringComparison.OrdinalIgnoreCase): allDoctor.Add(doctor.Patronymic); break;
                        case true when string.Equals("NumberPhone", selestBy, StringComparison.OrdinalIgnoreCase): allDoctor.Add(doctor.NumberPhone); break;
                    }

                }
                model.Filter = allDoctor;
                model.NameFilter = filter;
                return View(model);
            }
            else if (filter.Contains("Patient ", StringComparison.OrdinalIgnoreCase) || filter.Contains("Residence Address", StringComparison.OrdinalIgnoreCase) || filter.Contains("Gender", StringComparison.OrdinalIgnoreCase))
            {
                var model = new ViewModelForFilter();
                var allPatient = new List<string>();
                var allCardInDataBase = CardServices.registrationCardRepository.Get();
                foreach (var element in allCardInDataBase)
                {
                    var selestBy = filter.Replace("Patient ", string.Empty, StringComparison.OrdinalIgnoreCase).Replace(" ", string.Empty);
                    var patient = CardServices.patientRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                    switch (true)
                    {
                        case true when string.Equals("FirstName", selestBy, StringComparison.OrdinalIgnoreCase): allPatient.Add(patient.FirstName); break;
                        case true when string.Equals("LastName", selestBy, StringComparison.OrdinalIgnoreCase): allPatient.Add(patient.LastName); break;
                        case true when string.Equals("Patronymic", selestBy, StringComparison.OrdinalIgnoreCase): allPatient.Add(patient.Patronymic); break;
                        case true when string.Equals("Gender", selestBy, StringComparison.OrdinalIgnoreCase): allPatient.Add(patient.Gender); break;
                        case true when string.Equals("ResidenceAddress", selestBy, StringComparison.OrdinalIgnoreCase): allPatient.Add(patient.ResidenceAddress); break;
                    }

                }
                model.Filter = allPatient;
                model.NameFilter = filter;
                return View(model);
            }
            else if (filter.Contains("Diagnosis", StringComparison.OrdinalIgnoreCase))
            {
                var model = new ViewModelForFilter();
                var allDiagnosis = new List<string>();
                var allCardInDataBase = CardServices.registrationCardRepository.Get();
                foreach (var element in allCardInDataBase)
                {
                    var patient = CardServices.medicalHistoryRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                    switch (true)
                    {
                        case true when string.Equals("Diagnosis", filter, StringComparison.OrdinalIgnoreCase): allDiagnosis.Add(patient.Diagnosis); break;
                    }

                }
                model.Filter = allDiagnosis;
                model.NameFilter = filter;
                return View(model);
            }
            else if (filter.Contains("Date Admission", StringComparison.OrdinalIgnoreCase) || filter.Contains("DateAdmission", StringComparison.OrdinalIgnoreCase))
            {
                var model = new ViewModelForFilter();
                var allCard = new List<string>();
                var allCardInDataBase = CardServices.registrationCardRepository.Get();
                foreach (var element in allCardInDataBase)
                {
                    var selestBy = filter.Replace("eA", "e A", StringComparison.OrdinalIgnoreCase);
                    var card = CardServices.registrationCardRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                    switch (true)
                    {
                        case true when string.Equals("Date Admission", selestBy, StringComparison.OrdinalIgnoreCase): allCard.Add(card.DateAdmission.ToShortDateString()); break;
                      
                    }

                }
                model.Filter = allCard;
                model.NameFilter = filter;
                return View(model);
            }    
            return BadRequest("Вы вели фильтр не верно или такого фильтра не существует");
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

        [HttpGet]
        public IActionResult Remove(ViewModelBaseTable request)
        {
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var card = CardServices.registrationCardRepository.Get().FirstOrDefault(el => el.Id == id);
            await CardServices.registrationCardRepository.Delete(card);
            return RedirectToAction(nameof(GetAllCard));
        }
    }
       
}
