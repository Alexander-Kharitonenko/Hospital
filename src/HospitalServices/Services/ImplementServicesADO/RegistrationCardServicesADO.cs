using Hospital.DataAccess.ADO;
using Hospital.DataAccess.Entity;
using RepositoryADO.InterfaceForRepository;
using Services.InterfaceServicec;
using Services.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImplementServicesADO
{
    public class RegistrationCardServicesADO : IRegistrationCardServices
    {
        public readonly RegistrationCardRepositoryADO Context;
        public RegistrationCardServicesADO(RegistrationCardRepositoryADO context) 
        {
            Context = context;
        }

        public async Task Add(RegistrationCard registrationCard)
        {
            await Context.CreateEntity(registrationCard);
        }

        public async Task DeleteRegistrationCard(RegistrationCard registrationCard)
        {
           await Context.Delete(registrationCard);
        }

        public IEnumerable<RegistrationCard> GedRegistrationCardById(int Id)
        {
           var result = Context.GetAllEntityBy(el => el.Id == Id);
           return result;
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
