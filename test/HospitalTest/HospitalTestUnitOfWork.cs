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
        


        [SetUp]
        public void Stert()
        {
            config.LoadDataBase();
            
        }

        [Test]
        public async Task SaveChanges_WenAddEntity_ThenSaveChangesEntity()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            DbContextOptions<HospitalContext> options = optionsBuilder.UseSqlServer(config.ConnectionString).Options;

            using (HospitalContext context = new HospitalContext(options))
            {
                IUnitOfWork repositorys = new UnitOfWork(new DoctorRepository(context), new MedicalHistoryRepository(context), new PatientRepository(context), new RegistrationCardRepository(context), context);
                await repositorys.doctorRepository.CreateEntity(DoctorData);
                await repositorys.registrationCardRepository.CreateEntity(RegistrationCardData);
                var result = await repositorys.SaveChangesAsync();
                Assert.NotNull(result);
            }

            //Act

           
            //Assert

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

        //[TearDown]
        //public void End()
        //{
        //    config.DropDataBase();
        //}
    }
}
