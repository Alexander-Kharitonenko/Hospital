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
    /// class for test DoctorRepositoryAdo
    /// </summary>
    public class HospitalTestDoctorRepositoryAdo
    {
        /// <summary>
        ///object for database management
        /// </summary>
        DataBaseConfigurationManager Config = new DataBaseConfigurationManager();

        /// <summary>
        /// list with reference values ​​for comparison
        /// </summary>
        List<Doctor> ComparisonList = new List<Doctor>()
        {
                new Doctor() { Id = 1, LastName ="Gulagina", Patronymic ="Anatolyevna", FirstName ="Julia", NumberPhone ="+ 375251111111"},
                new Doctor() { Id = 2, LastName ="Vasiliev", Patronymic ="Valentinovich", FirstName ="Valery", NumberPhone ="+ 375252222222"},
                new Doctor() { Id = 3, LastName ="Ugarov",Patronymic ="Mikhailovich", FirstName ="Victor", NumberPhone ="+ 375253333333"},
                new Doctor() { Id = 4, LastName ="Demchuk", Patronymic ="Pavlovich", FirstName ="Alexey", NumberPhone ="+ 375254444444"},
                new Doctor() { Id = 5, LastName ="Grishina",  Patronymic ="Konstantinovna", FirstName ="Olga", NumberPhone ="+ 375255555555"},
        };
    
        /// <summary>
        /// initial data for DoctorData
        /// </summary>
        private const int ARBITRARY_VALUE_ID = 3;

        /// <summary>
        /// initial data
        /// </summary>
        public static Doctor DoctorData
        {
            get
            {
                return new Doctor() { Id = ARBITRARY_VALUE_ID, FirstName = "TestName", Patronymic = "TestPatronymic", LastName = "TestLastName", NumberPhone = "TestNumberPhone" };
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
        public void Get_WhenGetDoctor_ThenGetDoctor()
        {
            // Arrange
            var ArbitraryValueIndex = 4;
            var doctorRepositoryAdo = new DoctorRepositoryAdo(Config.ConnectionString);

            // Act
            var result = doctorRepositoryAdo.Get().ToList();

            // Assert
            Assert.AreEqual(result[ArbitraryValueIndex].LastName , ComparisonList[ArbitraryValueIndex].LastName);
        }

        /// <summary>
        /// method to test Create method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public async Task CreateEntity_WhenAddingDoctor_ThenDoctorAdded()
        {
            // Arrange
            var ArbitraryValueIndex = 5;
            var doctor = new List<Doctor>();
            var doctorRepositoryAdo = new DoctorRepositoryAdo(Config.ConnectionString);

            // Act
            await doctorRepositoryAdo.CreateEntity(DoctorData);
            doctor.AddRange(doctorRepositoryAdo.Get());
            var result = doctor[ArbitraryValueIndex];

            // Assert
            Assert.AreEqual(result.LastName, DoctorData.LastName);
        }

        /// <summary>
        /// method to test Update method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task UpdateDoctor_WhenDoctorApdates_ThenDoctorUpdated()
        {
            // Arrange
            var ArbitraryValueIndex = 2;
            var doctor = new List<Doctor>();
            var doctorRepositoryAdo = new DoctorRepositoryAdo(Config.ConnectionString);

            // Act
            await doctorRepositoryAdo.Update(DoctorData);
            doctor.AddRange(doctorRepositoryAdo.Get());

            // Assert
            Assert.AreEqual(doctor[ArbitraryValueIndex].LastName, DoctorData.LastName);
        }

        /// <summary>
        ///  method to test Delete method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Delete_WhenIdDoctor_ThenDeleteDoctor()
        {
            // Arrange
            const int allRecordsAfterDeletion = 4;
            var doctor = new List<Doctor>();
            var doctorRepositoryAdo = new DoctorRepositoryAdo(Config.ConnectionString);

            //Act    
            await doctorRepositoryAdo.Delete(DoctorData);
            doctor.AddRange(doctorRepositoryAdo.Get());

            //Assert
            Assert.AreEqual(allRecordsAfterDeletion, doctor.Count);
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
