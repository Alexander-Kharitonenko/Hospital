using Hospital.DataAccess.Interfaces;
using Hospital.Services.InterfaceServices;
using HospitalMVCApplication.Models.ModelForRegistrationCard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork CardRepository;
        private readonly IRegistrationCardServices CardServices;

        public HomeController(IUnitOfWork cardRepository, IRegistrationCardServices cardServices)
        {
            CardRepository = cardRepository;
            CardServices = cardServices;
        }

        [HttpGet]
        public IActionResult GetAllCard()
        {
            ViewModelAllCard allCard = new ViewModelAllCard();
            var baseTable = new List<ViewModelBaseTable>();

            var allCardInDataBase = CardServices.GetAllRegistrationCard();
            foreach (var element in allCardInDataBase)
            {

                ViewModelBaseTable baseTabse = new ViewModelBaseTable()
                {
                    Id = element.Id,
                    Doctor = element.Doctor,
                    Patient = element.Patient,
                    DateAdmission = element.DateAdmission.ToShortDateString(),
                    Diagnosis = element.Diagnosis
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
                return RedirectToAction(nameof(CardByFilter), "Home", new { filter = request.NameFilter });
            }


            return View(request);
        }


        [HttpGet]
        public IActionResult CardByFilter(string filter)
        {
            

                if (CardServices.GetAllRegistrationCard().Any(el => el.Doctor.FirstName.Contains(filter)))
                {
                    var cards = CardServices.GetAllRegistrationCard().Where(el => el.Doctor.FirstName.Contains(filter));
                    IEnumerable<string> result = cards.Select(el => el.Doctor.FirstName);
                    ViewModelForFilter model = new ViewModelForFilter() { Filter = result };
                    return View(model);
                }
            

            return BadRequest("Вы вели фильтр не верно или такого фильтра не существует");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var CardWithDataBase = CardServices.GetAllRegistrationCard().FirstOrDefault(el => el.Id == id);
            ViewModelBaseTable baseTabse = new ViewModelBaseTable()
            {
                Id = CardWithDataBase.Id,
                Doctor = CardWithDataBase.Doctor,
                Patient = CardWithDataBase.Patient,
                DateAdmission = CardWithDataBase.DateAdmission.ToString(),
                Diagnosis = CardWithDataBase.Diagnosis
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
                new CookieOptions
                {
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
            var card = CardRepository.registrationCardRepository.Get().FirstOrDefault(el => el.Id == id);
            await CardServices.DeleteRegistrationCard(card);
            return RedirectToAction(nameof(GetAllCard));
        }
    }

}
