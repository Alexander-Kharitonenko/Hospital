using Hospital.DataAccess;
using Hospital.DataAccess.Entity;
using Hospital.DataAccess.RepositoryEntityFramework;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;

namespace HospitalTest
{
    /// <summary>
    /// class for test RepositoryEF
    /// </summary>
    public class HospitalTestRegistrationCardRepositoryEntityFramework
    {
        /// <summary>
        ///object for database management
        /// </summary>
        DataBaseConfigurationManager Config = new DataBaseConfigurationManager();

        /// <summary>
        /// initial data for RegistrationCardData
        /// </summary>
        private const int ARBITRARY_VALUE__DOCTOD_ID = 3;

        /// <summary>
        /// initial data for RegistrationCardData
        /// </summary>
        private const int ARBITRARY_VALUE__PATIENT_ID = 4;

        /// <summary>
        /// initial data for RegistrationCardData
        /// </summary>
        private const int ARBITRARY_VALUE__DIAGNOSIS_Id = 2;

        /// <summary>
        /// initial data
        /// </summary>
        public static RegistrationCard RegistrationCardData
        {
            get
            {
                return new RegistrationCard() { DoctorId = ARBITRARY_VALUE__DOCTOD_ID, PatientId = ARBITRARY_VALUE__PATIENT_ID, DiagnosisId = ARBITRARY_VALUE__DIAGNOSIS_Id, DateAdmission = DateTime.UtcNow.Date };
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
        /// list with reference values ​​for comparison
        /// </summary>
        List<RegistrationCard> ComparisonList = new List<RegistrationCard>()
        {
                new RegistrationCard() { Id = 1, DoctorId = 2 , PatientId = 3 , DateAdmission=new DateTime(2021,06, 11 ), DiagnosisId = 1},
                new RegistrationCard() { Id = 2, DoctorId = 3 , PatientId = 2 , DateAdmission=new DateTime(2021,01, 03 ) , DiagnosisId = 5},
                new RegistrationCard() { Id = 3,  DoctorId = 1 , PatientId = 5 , DateAdmission=new DateTime(2021,01, 10 ), DiagnosisId = 3},
                new RegistrationCard() { Id = 4, DoctorId = 5, PatientId = 1 , DateAdmission= new DateTime(2021,9, 21 ), DiagnosisId = 4},
                new RegistrationCard() { Id = 5, DoctorId = 4 , PatientId = 4 , DateAdmission= new DateTime(2021,12, 12 ), DiagnosisId = 2},
        };

        /// <summary>
        /// runs at the beginning of the test and creates the database
        /// </summary>
        /// <returns>void</returns>
        [SetUp]
        public void Start()
        {
            Config.LoadDataBase();
        }

        /// <summary>
        /// method to test Get method
        /// </summary>
        /// <returns>IEnumerable<RegistrationCard></returns>
        [Test]
        public void Get_WhenGet_ThenGetAllEntity()
        {
            // Arrange
            var arbitraryValueIndex = 4;
            List<RegistrationCard> result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var registrationCardRepository = new RegistrationCardRepository(context);
                result = registrationCardRepository.Get().ToList();
            }

            // Assert
            Assert.AreEqual(result[arbitraryValueIndex].DateAdmission, ComparisonList[arbitraryValueIndex].DateAdmission);
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
            const int numberOfChanges = 1;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            //Act
            using (var context = new HospitalContext(options))
            {
                var repositorys = new UnitOfWork(new DoctorRepository(context), new MedicalHistoryRepository(context), new PatientRepository(context), new RegistrationCardRepository(context), context);
                await repositorys.doctorRepository.CreateEntity(DoctorData);
                await repositorys.registrationCardRepository.CreateEntity(RegistrationCardData);
                result = await repositorys.SaveChangesAsync();
            }

            //Assert
            Assert.AreEqual(numberOfChanges, result);
        }

        /// <summary>
        /// method to test Create method
        /// </summary>
        /// <returns>IEnumerable<RegistrationСard></returns>
        [Test]
        public async Task Create_WhenAddingRegistrationСard_ThenRegistrationСardAdd()
        {
            // Arrange
            const int numberOfChanges = 1;
            int result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var registrationCardRepository = new RegistrationCardRepository(context);
                await registrationCardRepository.CreateEntity(RegistrationCardData);
                result = await registrationCardRepository.SaveChanges();
            }

            // Assert
            Assert.AreEqual(numberOfChanges, result);
        }

        /// <summary>
        ///  method to test Delete method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Delete_WhenRegistrationСard_ThenDeleteRegistrationСard()
        {
            // Arrange
            const int numberOfChanges = 1;
            int randomValueId = 3;
            int result;
            var card = new RegistrationCard() { Id = randomValueId };
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var registrationCardRepository = new RegistrationCardRepository(context);
                await registrationCardRepository.Delete(card);
                result = await registrationCardRepository.SaveChanges();
            }

            // Assert
            Assert.AreEqual(numberOfChanges, result);
        }

        /// <summary>
        /// method to test Update method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Update_WhenRegistrationСard_ThenUpdateRegistrationСard()
        {
            int randomValueId = 3;
            int randomValueDoctorId = 3;
            int randomValuePatientId = 4;
            int randomValueDiagnosisId = 2;
            const int numberOfChanges = 1;
            int result;
            var card = new RegistrationCard() { Id = randomValueId, DoctorId = randomValueDoctorId, PatientId = randomValuePatientId, DiagnosisId = randomValueDiagnosisId, DateAdmission = DateTime.UtcNow.Date };
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var registrationCardRepository = new RegistrationCardRepository(context);
                await registrationCardRepository.Update(card);
                result = await registrationCardRepository.SaveChanges();
            }

            // Assert
            Assert.AreEqual(numberOfChanges, result);
        }

        /// <summary>
        /// runs at the end of the test and drops the database
        /// </summary>
        [TearDown]
        public void End()
        {
            Config.DropDataBase();
        }
    }
}
