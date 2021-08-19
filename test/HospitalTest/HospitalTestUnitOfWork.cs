using DataAccess.Entity;
using Hospital.DataAccess;
using Hospital.DataAccess.ADO;
using Hospital.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;

namespace HospitalTest
{
    public class HospitalTestUnitOfWork
    {
        DataBaseConfigurationManager config = new DataBaseConfigurationManager();
        DbContextOptionsBuilder<HospitalContext> optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();


        

        [SetUp]
        public void Stert()
        {
            config.LoadDataBase();
            
        }

        [Test]
        public async Task SaveChanges_WenAddEntity_ThenSaveChangesEntity()
        {
            // Arrange

            DbContextOptions<HospitalContext> options = optionsBuilder.UseSqlServer(config.ConnectionString).Options;

            IUnitOfWork repositorys = new UnitOfWork(new DoctorRepository(new HospitalContext(options)), new MedicalHistoryRepository(new HospitalContext(options)), new PatientRepository(new HospitalContext(options)), new RegistrationCardRepository(new HospitalContext(options)));

            //Act

            await repositorys.doctorRepository.CreateEntity(DoctorData);
            await repositorys.registrationCardRepository.CreateEntity(RegistrationCardData);
            var result = await repositorys.SaveChangesAsync();

            //Assert

            Assert.NotNull(result);



        }


        public static RegistrationCard RegistrationCardData
        {
            get
            {
                return new RegistrationCard() { Id = 3, PatientId = 3, DoctorId = 3, DiagnosisId = 2, DateAdmission = DateTime.UtcNow.Date };
            }
        }

        public static Doctor DoctorData
        {
            get
            {
                return new Doctor() { Id = 3, FirstName = "TestName", Patronymic = "TestPatronymic", LastName = "TestLastName", NumberPhone = "TestNumberPhone" };
            }
        }

        [TearDown]
        public void End()
        {
            config.DropDataBase();
        }
    }
}
