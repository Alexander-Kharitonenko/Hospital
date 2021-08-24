using Hospital.DataAccess.Entity;
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = DoctorServices.doctorRepository.GetAllEntityBy(el => el.Id == id);
            return View(result.FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Edit(Doctor request)
        {
            Doctor doc = new Doctor()
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NumberPhone = request.NumberPhone,
                Patronymic = request.Patronymic
            };
            DoctorServices.doctorRepository.Update(doc);

            return View(request);
        }

    }
}
