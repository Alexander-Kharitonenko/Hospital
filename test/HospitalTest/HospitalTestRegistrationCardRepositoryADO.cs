using Hospital.DataAccess.RepositoryAdo;
using Hospital.DataAccess.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;
using System.Linq;

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
        DataBaseConfigurationManager Config = new DataBaseConfigurationManager();

        /// <summary>
        /// initial data RegistrationCardData
        /// </summary>
        private const int ARBITRARY_VALUE_ID = 3;

        /// <summary>
        /// initial data RegistrationCardData
        /// </summary>
        private const int ARBITRARY_VALUE_PATIENT_ID = 3;

        /// <summary>
        /// initial data RegistrationCardData
        /// </summary>
        private const int ARBITRARY_VALUE_DOCTOR_ID = 3;

        /// <summary>
        /// initial data RegistrationCardData
        /// </summary>
        private const int ARBITRARY_VALUE_DIAGNOSIS_ID = 2;

        /// <summary>
        /// initial data
        /// </summary>
        public static RegistrationCard RegistrationCardData
        {
            get
            {
                return new RegistrationCard() { Id = ARBITRARY_VALUE_ID, PatientId = ARBITRARY_VALUE_PATIENT_ID, DoctorId = ARBITRARY_VALUE_DOCTOR_ID, DiagnosisId = ARBITRARY_VALUE_DIAGNOSIS_ID, DateAdmission = DateTime.UtcNow.Date };
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
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public void Get_WhenGet_ThenReturnAllRegistrationCard()
        {
            // Arrange
            const int ARBITRARY_VALUE_INDEX = 4;
            var registrationCardRepositoryAdo = new RegistrationCardRepositoryAdo(Config.ConnectionString);

            // Act
            var result = registrationCardRepositoryAdo.Get().ToList();

            // Assert
            Assert.AreEqual(result[ARBITRARY_VALUE_INDEX].DateAdmission, ComparisonList[ARBITRARY_VALUE_INDEX].DateAdmission);
        }

        /// <summary>
        /// method to test Create method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public async Task CreateEntity_WhenRegistrationCar_ThenCreateRegistrationCar()
        {
            // Arrange
            const int ARBITRARY_VALUE_INDEX = 2;
            var registrationCards = new List<RegistrationCard>();
            var registrationCardRepositoryAdo = new RegistrationCardRepositoryAdo(Config.ConnectionString);

            // Act
            await registrationCardRepositoryAdo.CreateEntity(RegistrationCardData);
            registrationCards.AddRange(registrationCardRepositoryAdo.Get());
            var result = registrationCards[ARBITRARY_VALUE_INDEX];

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
            const int ARBITRARY_VALUE_INDEX = 2;
            var registrationCards = new List<RegistrationCard>();
            var registrationCardRepositoryAdo = new RegistrationCardRepositoryAdo(Config.ConnectionString);

            // Act
            await registrationCardRepositoryAdo.Update(RegistrationCardData);
            registrationCards.AddRange(registrationCardRepositoryAdo.Get());
            var result = registrationCards[ARBITRARY_VALUE_INDEX];

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
            const int ALL_RECORDS_AFTER_DELETION = 4;
            var registrationCards = new List<RegistrationCard>();
            var registrationCardRepositoryAdo = new RegistrationCardRepositoryAdo(Config.ConnectionString);

            // Act
            await registrationCardRepositoryAdo.Delete(RegistrationCardData);
            registrationCards.AddRange(registrationCardRepositoryAdo.Get());

            // Assert
            Assert.AreEqual(ALL_RECORDS_AFTER_DELETION, registrationCards.Count);
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
