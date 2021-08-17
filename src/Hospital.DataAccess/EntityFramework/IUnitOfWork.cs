using DataAccess.Entity;
using Repository.InterfaceForRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ImplementationRepository
{
    public interface IUnitOfWork
    {
        
        IDoctorRepository doctorRepository { get; }
        IMedicalHistoryRepository medicalHistoryRepository { get; }
        IPatientRepository patientRepository { get; }
        IRegistrationCardRepository registrationCardRepository { get; }
    }
}
