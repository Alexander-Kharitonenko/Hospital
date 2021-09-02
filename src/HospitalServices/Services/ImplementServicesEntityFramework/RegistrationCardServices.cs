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
        public IRegistrationCardRepository Context;
        public RegistrationCardServices(IRegistrationCardRepository context) 
        {
            Context = context;
        }

        public async Task Add(RegistrationCard registrationCard)
        {
            await Context.CreateEntity(registrationCard);
            await Context.SaveChanges();
        }

        public async Task DeleteRegistrationCard(RegistrationCard registrationCard)
        {
            await Context.Delete(registrationCard);
            await Context.SaveChanges();
        }

        public IEnumerable<RegistrationCard> GetAllRegistrationCard()
        {
            var result = Context.Get();
            return result;
        }

        public async Task UpdateRegistrationCard(RegistrationCard registrationCard)
        {
            await Context.Update(registrationCard);
            await Context.SaveChanges();
        }
    }
}
