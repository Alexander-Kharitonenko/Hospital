using Hospital.DataAccess;
using Hospital.DataAccess.ADO;
using Hospital.DataAccess.Entity;
using Hospital.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;

namespace HospitalTest
{
    public class HospitalTestRepositoryEF
    {
        DataBaseConfigurationManager config = new DataBaseConfigurationManager();
        

        [SetUp]
        public void Start()
        {
            config.LoadDataBase();    
        }

        [Test]
        public void Get_WhenGet_ThenGetAllEntity()
        {
            // Arrange
            IEnumerable<RegistrationCard> result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            DbContextOptions<HospitalContext> options = optionsBuilder.UseSqlServer(config.ConnectionString).Options;

            // Act 
            using (HospitalContext ct = new HospitalContext(options))
            {
                RegistrationCardRepository doc = new RegistrationCardRepository(ct);
                result = doc.Get();
            }

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public void GetBy_WhenGetByName_ThenGetAllEntityWhithName()
        {
            // Arrange
            IEnumerable<RegistrationCard> result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            DbContextOptions<HospitalContext> options = optionsBuilder.UseSqlServer(config.ConnectionString).Options;

            // Act 
            using (HospitalContext ct = new HospitalContext(options))
            {
                RegistrationCardRepository doc = new RegistrationCardRepository(ct);
                result = doc.GetAllEntityBy(el => el.Id == 4);

            }

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task SaveChanges_WenAddEntity_ThenSaveChangesEntity()
        {
            // Arrange
            int result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            DbContextOptions<HospitalContext> options = optionsBuilder.UseSqlServer(config.ConnectionString).Options;

            //Act
            using (HospitalContext context = new HospitalContext(options))
            {
                IUnitOfWork repositorys = new UnitOfWork(new DoctorRepository(context), new MedicalHistoryRepository(context), new PatientRepository(context), new RegistrationCardRepository(context), context);
                await repositorys.doctorRepository.CreateEntity(DoctorData);
                await repositorys.registrationCardRepository.CreateEntity(RegistrationCardData);
                result = await repositorys.SaveChangesAsync();
              
            }

            //Assert
             Assert.NotNull(result);
        }

        [Test]
        public async Task CreateEFCore_WhenAddingRegistrationСard_ThenRegistrationСardAdd()
        {
            // Arrange
            int result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            DbContextOptions<HospitalContext> options = optionsBuilder.UseSqlServer(config.ConnectionString).Options;

            // Act 
            using (HospitalContext ct = new HospitalContext(options))
            {
                RegistrationCardRepository Rc = new RegistrationCardRepository(ct);
                await Rc.CreateEntity(RegistrationCardData);
                result = await Rc.SaveChanges();

            }

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task DeleteEFCore_WhenRegistrationСard_ThenDeleteRegistrationСard()
        {
            // Arrange
            int result;
            RegistrationCard card = new  RegistrationCard() {Id = 3};
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            DbContextOptions<HospitalContext> options = optionsBuilder.UseSqlServer(config.ConnectionString).Options;

            // Act 
            using (HospitalContext ct = new HospitalContext(options))
            {

                RegistrationCardRepository Rc = new RegistrationCardRepository(ct);
                await Rc.Delete(card);
                result = await Rc.SaveChanges();

            }

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task UpdateEFCore_WhenRegistrationСard_ThenUpdateRegistrationСard()
        {
            // Arrange
            int result;
            RegistrationCard card = new RegistrationCard() { Id = 3 , DoctorId = 4, PatientId = 5, DiagnosisId = 4, DateAdmission = DateTime.UtcNow.Date };
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            DbContextOptions<HospitalContext> options = optionsBuilder.UseSqlServer(config.ConnectionString).Options;

            // Act 
            using (HospitalContext ct = new HospitalContext(options))
            {

                RegistrationCardRepository Rc = new RegistrationCardRepository(ct);
                await Rc.Update(card);
                result = await Rc.SaveChanges();

            }

            // Assert
            Assert.AreEqual(1, result);
        }


        public static RegistrationCard RegistrationCardData
        {
            get
            {
                return new RegistrationCard() { DoctorId = 3 , PatientId = 4, DiagnosisId = 2, DateAdmission = DateTime.UtcNow.Date };
            }
        }

        public static Doctor DoctorData
        {
            get
            {
                return new Doctor() { FirstName = "TestName", Patronymic = "TestPatronymic", LastName = "TestLastName", NumberPhone = "TestNumberPhone" };
            }
        }

        [TearDown]
        public void End()
        {
            config.DropDataBase();
        }
    }
}
