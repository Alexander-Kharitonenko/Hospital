using Hospital.DataAccess.RepositoryAdo;
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
            var patientRepositoryAdo = new PatientRepositoryAdo(config.ConnectionString);

            // Act
            var result = patientRepositoryAdo.Get();

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
            var patientRepositoryAdo = new PatientRepositoryAdo(config.ConnectionString);

            // Act
            await patientRepositoryAdo.CreateEntity(PatientData);
            patients.AddRange(patientRepositoryAdo.Get());
            var result = patients[5];

            // Assert
            Assert.AreEqual(result.LastName, PatientData.LastName);
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
            var patientRepositoryAdo = new PatientRepositoryAdo(config.ConnectionString);

            // Act
            await patientRepositoryAdo.Update(PatientData);
            patients.AddRange(patientRepositoryAdo.Get());
            var result = patients[2];

            // Assert
            Assert.AreEqual(result.LastName, PatientData.LastName);
        }

        /// <summary>
        ///  method to test Delete method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Delete_WhenPatient_ThenDeletePatient()
        {
            // Arrange
            const int allRecordsAfterDeletion = 4;
            var patients = new List<Patient>();
            var patientRepositoryAdo = new PatientRepositoryAdo(config.ConnectionString);

            // Act
            await patientRepositoryAdo.Delete(PatientData);
            patients.AddRange(patientRepositoryAdo.Get());

            //Assert
            Assert.AreEqual(allRecordsAfterDeletion, patients.Count);
        }

        /// <summary>
        /// initial data PatientData
        /// </summary>
        private const int _id = 3;

        /// <summary>
        /// initial data
        /// </summary>
        public static Patient PatientData
        {
            get
            {
                return new Patient() { Id = _id, FirstName = "TestFirstName", Patronymic = "TestPatronymic", LastName = "TestLastName", Gender = "TestGender", ResidenceAddress = "TestResidenceAddress" };
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
