using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;
using HospitalMVCApplication.Models.ModelForDoctor;
using Microsoft.AspNetCore.Mvc;
using Hospital.Services.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMVCApplication.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorServices DoctorServices; 

        public DoctorController(IDoctorServices doctorServices) 
        {
            DoctorServices = doctorServices;
        }

        [HttpGet]
        public IActionResult GetAllDoctor()
        {
            ViewModelForDoctor model = new ViewModelForDoctor() { AllDoctors = DoctorServices.GetAllDoctor() };
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = DoctorServices.GetAllDoctor();
            return View(result.FirstOrDefault(el => el.Id == id));
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
            DoctorServices.UpdateDoctor(doc);

            return View(request);
        }

        [HttpGet]
        public IActionResult Remove(DoctorDTO request)
        {
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var doctor = DoctorServices.GetAllDoctor().FirstOrDefault(el => el.Id == id);
            await DoctorServices.DeleteDoctor(doctor);
            return RedirectToAction(nameof(GetAllDoctor));
        }
    }
}
