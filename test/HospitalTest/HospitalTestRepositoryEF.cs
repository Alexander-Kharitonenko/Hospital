using Hospital.DataAccess;
using Hospital.DataAccess.Entity;
using Hospital.DataAccess.EntityFramework;
using Hospital.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;

namespace HospitalTest
{
    /// <summary>
    /// class for test RepositoryEF
    /// </summary>
    public class HospitalTestRepositoryEF
    {
        /// <summary>
        ///object for database management
        /// </summary>
        DataBaseConfigurationManager config = new DataBaseConfigurationManager();

        /// <summary>
        /// runs at the beginning of the test and creates the database
        /// </summary>
        /// <returns>void</returns>
        [SetUp]
        public void Start()
        {
            config.LoadDataBase();
        }

        /// <summary>
        /// method to test Get method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public void Get_WhenGet_ThenGetAllEntity()
        {
            // Arrange
            IEnumerable<RegistrationCard> result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(config.ConnectionString).Options;

            // Act 
            using (var ct = new HospitalContext(options))
            {
                var doc = new RegistrationCardRepository(ct);
                result = doc.Get();
            }

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        ///  method to Save Changes
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task SaveChanges_WenAddEntity_ThenSaveChangesEntity()
        {
            // Arrange
            int result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(config.ConnectionString).Options;

            //Act
            using (var context = new HospitalContext(options))
            {
                var repositorys = new UnitOfWork(new DoctorRepository(context), new MedicalHistoryRepository(context), new PatientRepository(context), new RegistrationCardRepository(context), context);
                await repositorys.doctorRepository.CreateEntity(DoctorData);
                await repositorys.registrationCardRepository.CreateEntity(RegistrationCardData);
                result = await repositorys.SaveChangesAsync();
            }

            //Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// method to test Create method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public async Task CreateEFCore_WhenAddingRegistrationСard_ThenRegistrationСardAdd()
        {
            // Arrange
            int result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(config.ConnectionString).Options;

            // Act 
            using (var ct = new HospitalContext(options))
            {
                var Rc = new RegistrationCardRepository(ct);
                await Rc.CreateEntity(RegistrationCardData);
                result = await Rc.SaveChanges();
            }

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        ///  method to test Delete method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task DeleteEFCore_WhenRegistrationСard_ThenDeleteRegistrationСard()
        {
            // Arrange
            int result;
            var card = new RegistrationCard() { Id = 3 };
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(config.ConnectionString).Options;

            // Act 
            using (var ct = new HospitalContext(options))
            {

                var Rc = new RegistrationCardRepository(ct);
                await Rc.Delete(card);
                result = await Rc.SaveChanges();

            }

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// method to test Update method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task UpdateEFCore_WhenRegistrationСard_ThenUpdateRegistrationСard()
        {
            // Arrange
            int result;
            var card = new RegistrationCard() { Id = 3, DoctorId = 4, PatientId = 5, DiagnosisId = 4, DateAdmission = DateTime.UtcNow.Date };
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(config.ConnectionString).Options;

            // Act 
            using (var ct = new HospitalContext(options))
            {

                var Rc = new RegistrationCardRepository(ct);
                await Rc.Update(card);
                result = await Rc.SaveChanges();

            }

            // Assert
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// initial data
        /// </summary>
        public static RegistrationCard RegistrationCardData
        {
            get
            {
                return new RegistrationCard() { DoctorId = 3, PatientId = 4, DiagnosisId = 2, DateAdmission = DateTime.UtcNow.Date };
            }
        }

        /// <summary>
        /// initial data
        /// </summary>
        public static Doctor DoctorData
        {
            get
            {
                return new Doctor() { FirstName = "TestName", Patronymic = "TestPatronymic", LastName = "TestLastName", NumberPhone = "TestNumberPhone" };
            }
        }

        /// <summary>
        /// runs at the end of the test and drops the database
        /// </summary>
        [TearDown]
        public void End()
        {
            config.DropDataBase();
        }
    }
}
