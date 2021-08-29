using Hospital.DataAccess.RepositoryAdo;
using Hospital.DataAccess.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;

namespace Hospital.XUnitTest
{
    /// <summary>
    /// class for test RegistrationCardRepositoryAdo
    /// </summary>
    public class HospitalTestRegistrationCardRepositoryAdo
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
        public void Get_WhenGet_ThenReturnAllRegistrationCard()
        {
            // Arrange
            var registrationCardRepositoryAdo = new RegistrationCardRepositoryAdo(config.ConnectionString);

            // Act
            var result = registrationCardRepositoryAdo.Get();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// method to test Create method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public async Task CreateEntity_WhenRegistrationCar_ThenCreateRegistrationCar()
        {
            // Arrange
            var registrationCards = new List<RegistrationCard>();
            var registrationCardRepositoryAdo = new RegistrationCardRepositoryAdo(config.ConnectionString);

            // Act
            await registrationCardRepositoryAdo.CreateEntity(RegistrationCardData);
            registrationCards.AddRange(registrationCardRepositoryAdo.Get());
            var result = registrationCards[2];

            // Assert
            Assert.AreEqual(result.Id, RegistrationCardData.Id);
        }

        /// <summary>
        /// method to test Update method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Update_WhenRegistrationCard_ThenUpdateRegistrationCard()
        {
            // Arrange
            var registrationCards = new List<RegistrationCard>();
            var registrationCardRepositoryAdo = new RegistrationCardRepositoryAdo(config.ConnectionString);

            // Act
            await registrationCardRepositoryAdo.Update(RegistrationCardData);
            registrationCards.AddRange(registrationCardRepositoryAdo.Get());
            var result = registrationCards[2];

            // Assert
            Assert.AreEqual(RegistrationCardData.PatientId, registrationCards[0].PatientId);
        }

        /// <summary>
        ///  method to test Delete method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Delete_WhenRegistrationCard_ThenDeleteRegistrationCard()
        {
            // Arrange
            const int allRecordsAfterDeletion = 4;
            var registrationCards = new List<RegistrationCard>();
            var registrationCardRepositoryAdo = new RegistrationCardRepositoryAdo(config.ConnectionString);

            // Act
            await registrationCardRepositoryAdo.Delete(RegistrationCardData);
            registrationCards.AddRange(registrationCardRepositoryAdo.Get());

            // Assert
            Assert.AreEqual(allRecordsAfterDeletion, registrationCards.Count);
        }

        /// <summary>
        /// initial data RegistrationCardData
        /// </summary>
        private const int _id = 3;

        /// <summary>
        /// initial data RegistrationCardData
        /// </summary>
        private const int _patientId = 3;

        /// <summary>
        /// initial data RegistrationCardData
        /// </summary>
        private const int _doctorId = 3;

        /// <summary>
        /// initial data RegistrationCardData
        /// </summary>
        private const int _diagnosisId = 2;

        /// <summary>
        /// initial data
        /// </summary>
        public static RegistrationCard RegistrationCardData
        {
            get
            {
                return new RegistrationCard() { Id = _id, PatientId = _patientId, DoctorId = _doctorId, DiagnosisId = _diagnosisId, DateAdmission = DateTime.UtcNow.Date };
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
