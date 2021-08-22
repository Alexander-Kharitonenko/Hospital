
using Hospital.DataAccess.ADO;
using Hospital.DataAccess.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;

namespace Hospital.XUnitTest
{
    public class HospitalTestRegistrationCardRepositoryADO
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
            RegistrationCardRepositoryADO doc = new RegistrationCardRepositoryADO(config.ConnectionString);

            // Act
            IEnumerable<RegistrationCard> result = doc.Get();

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
            List<RegistrationCard> registrationCards = new List<RegistrationCard>();
            RegistrationCardRepositoryADO doc = new RegistrationCardRepositoryADO(config.ConnectionString);

            // Act
            await doc.CreateEntity(RegistrationCardData);
            var result = doc.GetAllEntityBy(el => el.Id == 6);
            foreach (var i in result)
            {
                registrationCards.Add(i);
            }

            // Assert
            Assert.AreEqual(RegistrationCardData.PatientId, registrationCards[0].PatientId);
        }

        /// <summary>
        /// method to test Update method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Update_WhenRegistrationCard_ThenUpdateRegistrationCard()
        {
            // Arrange
            List<RegistrationCard> registrationCards = new List<RegistrationCard>();
            RegistrationCardRepositoryADO doc = new RegistrationCardRepositoryADO(config.ConnectionString);

            // Act
            await doc.Update(RegistrationCardData);
            var result = doc.GetAllEntityBy(el => el.Id == 3);
            foreach (var i in result)
            {
                registrationCards.Add(i);
            }

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
            List<RegistrationCard> registrationCards = new List<RegistrationCard>();
            RegistrationCardRepositoryADO doc = new RegistrationCardRepositoryADO(config.ConnectionString);

            // Act
            await doc.Delete(RegistrationCardData);
            var result = doc.GetAllEntityBy(el => el.Id == 3);

            // Assert
            Assert.IsNull(result);
        }

        /// <summary>
        /// method to test GetBy method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public void GetAllEntityById_WhenId_5_ThenReturnRegistrationCardWhithId_5()
        {
            // Arrange
            RegistrationCardRepositoryADO doc = new RegistrationCardRepositoryADO(config.ConnectionString);

            // Act
            IEnumerable<RegistrationCard> result = doc.GetAllEntityBy(el => el.Id == 5);

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// initial data
        /// </summary>
        public static RegistrationCard RegistrationCardData
        {
            get
            {
                return new RegistrationCard() { Id = 3, PatientId = 3, DoctorId = 3, DiagnosisId = 2, DateAdmission = DateTime.UtcNow.Date };  
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
