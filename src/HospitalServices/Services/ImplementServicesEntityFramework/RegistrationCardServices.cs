using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;
using Hospital.Services.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.ImplementServicesEntityFramework
{
    public class RegistrationCardServices : IRegistrationCardServices
    {
        public IUnitOfWork Context;
        public RegistrationCardServices(IUnitOfWork context) 
        {
            Context = context;
        }

        public async Task Add(RegistrationCard registrationCard)
        {
            await Context.registrationCardRepository.CreateEntity(registrationCard);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteRegistrationCard(RegistrationCard registrationCard)
        {
            await Context.registrationCardRepository.Delete(registrationCard);
            await Context.SaveChangesAsync();
        }

        public IEnumerable<RegistrationCard> GetAllRegistrationCard()
        {
            var allRegistrationCard = new List<RegistrationCard>();
            var allCardInDataBase = Context.registrationCardRepository.Get();
            foreach (var element in allCardInDataBase)
            {
                var doctor = Context.doctorRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                var patient = Context.patientRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                var diagnosis = Context.medicalHistoryRepository.Get().FirstOrDefault(el => el.Id == element.Id);
                RegistrationCard card = new RegistrationCard()
                {
                    Id = element.Id,
                    Doctor = doctor,
                    Patient = patient,
                    Diagnosis = diagnosis,
                    DateAdmission = element.DateAdmission
                };
                allRegistrationCard.Add(card);
            }

            
            return allRegistrationCard;
        }

        public async Task UpdateRegistrationCard(RegistrationCard registrationCard)
        {
            await Context.registrationCardRepository.Update(registrationCard);
            await Context.SaveChangesAsync();
        }
    }
}
