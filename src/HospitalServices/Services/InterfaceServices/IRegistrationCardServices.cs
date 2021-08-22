using Hospital.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceServices
{
    public interface IRegistrationCardServices
    {
        public Task Add(RegistrationCard registrationCard);

        public IEnumerable<RegistrationCard> GedRegistrationCardById(int Id);

        public IEnumerable<RegistrationCard> GetAllRegistrationCard();

        public Task UpdateRegistrationCard(RegistrationCard registrationCard);

        public Task DeleteRegistrationCard(RegistrationCard registrationCard);
    }
}
