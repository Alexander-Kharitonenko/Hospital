using Hospital.DataAccess.ADO;
using Hospital.DataAccess.Entity;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;

namespace Hospital.XUnitTest
{
    /// <summary>
    /// class for test PatientRepositoryAdo
    /// </summary>
    public class HospitalTestPatientRepositoryAdo
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
        /// method to test get method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public void Get_WhenGet_ThenGetAllPatient()
        {
            // Arrange
            var doc = new PatientRepositoryAdo(config.ConnectionString);

            // Act
            var result = doc.Get();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// method to test Create method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public async Task CreateEntity_WhenCreatePatient_ThenCreatePatient()
        {
            // Arrange
            var patients = new List<Patient>();
            var doc = new PatientRepositoryAdo(config.ConnectionString);

            // Act
            await doc.CreateEntity(PatientData);
            var result = doc.GetAllEntityBy(el => el.Id == 6);
            foreach (var i in result)
            {
                patients.Add(i);
            }

            // Assert
            Assert.AreEqual(patients[0].LastName, PatientData.LastName);
        }

        /// <summary>
        /// method to test Update method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task UpdateEntity_WhenPatient_ThenUpdatePatient()
        {
            // Arrange
            var patients = new List<Patient>();
            var doc = new PatientRepositoryAdo(config.ConnectionString);

            // Act
            await doc.Update(PatientData);
            var result = doc.GetAllEntityBy(el => el.Id == 3);
            foreach (var i in result)
            {
                patients.Add(i);
            }

            // Assert
            Assert.AreEqual(patients[0].LastName, PatientData.LastName);
        }

        /// <summary>
        ///  method to test Delete method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Delete_WhenPatient_ThenDeletePatient()
        {
            // Arrange
            var patients = new List<Patient>();
            var doc = new PatientRepositoryAdo(config.ConnectionString);

            // Act
            await doc.Delete(PatientData);
            var result = doc.GetAllEntityBy(el => el.Id == 3);

            // Assert
            Assert.IsNull(result);
        }

        /// <summary>
        ///  method to test GetAllEntityById method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public void GetAllEntityById_WhenId_5_ThenReturnPatientWhithId_5()
        {
            // Arrange
            var doc = new PatientRepositoryAdo(config.ConnectionString);

            // Act
            var result = doc.GetAllEntityBy(el => el.Id == 5);

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// initial data
        /// </summary>
        public static Patient PatientData
        {
            get
            {
                return new Patient() { Id = 3, FirstName = "TestFirstName", Patronymic = "TestPatronymic", LastName = "TestLastName", Gender = "TestGender", ResidenceAddress = "TestResidenceAddress" };
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
