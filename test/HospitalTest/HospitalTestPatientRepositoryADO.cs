using Hospital.DataAccess.RepositoryAdo;
using Hospital.DataAccess.Entity;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;
using System.Linq;

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
        DataBaseConfigurationManager Config = new DataBaseConfigurationManager();

        /// <summary>
        /// list with reference values ​​for comparison
        /// </summary>
        List<Patient> ComparisonList = new List<Patient>()
        {
                new Patient() { Id = 1, LastName ="Smirnov", Patronymic ="Ivanovich", FirstName ="Sergey", Gender ="Male", ResidenceAddress="Gomel, Trudovaya st., 5"},
                new Patient() { Id = 2, LastName ="Lebedeva", Patronymic ="Nikolaevna", FirstName ="Natalya", Gender ="Female", ResidenceAddress="Gomel, Sadovaya st., 10"},
                new Patient() { Id = 3, LastName ="Solovyova",Patronymic ="Alexandrovna", FirstName ="Ksenia", Gender ="Female", ResidenceAddress="Mozyr Lesnaya st., 7"},
                new Patient() { Id = 4, LastName ="Orlov", Patronymic ="Viktorovich", FirstName ="Mikhail", Gender ="Male", ResidenceAddress="Mozyr Beregovaya st., 3"},
                new Patient() { Id = 5, LastName ="Kovalev",  Patronymic ="Anatolyevich", FirstName ="Igor", Gender ="Male", ResidenceAddress="Gomel, street Klenovaya, 1"},
        };

        /// <summary>
        /// initial data PatientData
        /// </summary>
        private const int ARBITRARY_VALUE_ID = 3;

        /// <summary>
        /// initial data
        /// </summary>
        public static Patient PatientData
        {
            get
            {
                return new Patient() { Id = ARBITRARY_VALUE_ID, FirstName = "TestFirstName", Patronymic = "TestPatronymic", LastName = "TestLastName", Gender = "TestGender", ResidenceAddress = "TestResidenceAddress" };
            }
        }

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
        /// method to test get method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public void Get_WhenGet_ThenGetAllPatient()
        {
            // Arrange
            var ArbitraryValueIndex = 4;
            var patientRepositoryAdo = new PatientRepositoryAdo(Config.ConnectionString);

            // Act
            var result = patientRepositoryAdo.Get().ToList();

            // Assert
            Assert.AreEqual(result[ArbitraryValueIndex].LastName, ComparisonList[ArbitraryValueIndex].LastName);
        }

        /// <summary>
        /// method to test Create method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public async Task CreateEntity_WhenCreatePatient_ThenCreatePatient()
        {
            // Arrange
            var ArbitraryValueIndex = 5;
            var patients = new List<Patient>();
            var patientRepositoryAdo = new PatientRepositoryAdo(Config.ConnectionString);

            // Act
            await patientRepositoryAdo.CreateEntity(PatientData);
            patients.AddRange(patientRepositoryAdo.Get());
            var result = patients[ArbitraryValueIndex];

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
            var ArbitraryValueIndex = 2;
            var patients = new List<Patient>();
            var patientRepositoryAdo = new PatientRepositoryAdo(Config.ConnectionString);

            // Act
            await patientRepositoryAdo.Update(PatientData);
            patients.AddRange(patientRepositoryAdo.Get());
            var result = patients[ArbitraryValueIndex];

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
            var patientRepositoryAdo = new PatientRepositoryAdo(Config.ConnectionString);

            // Act
            await patientRepositoryAdo.Delete(PatientData);
            patients.AddRange(patientRepositoryAdo.Get());

            //Assert
            Assert.AreEqual(allRecordsAfterDeletion, patients.Count);
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
