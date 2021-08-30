using Hospital.DataAccess.RepositoryAdo;
using Hospital.DataAccess.Entity;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;

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
            var doctorRepositoryAdo = new DoctorRepositoryAdo(Config.ConnectionString);

            // Act
            var result = doctorRepositoryAdo.Get();

            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// method to test Create method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public async Task CreateEntity_WhenAddingDoctor_ThenDoctorAdded()
        {
            // Arrange
            var doctor = new List<Doctor>();
            var doctorRepositoryAdo = new DoctorRepositoryAdo(Config.ConnectionString);

            // Act
            await doctorRepositoryAdo.CreateEntity(DoctorData);
            doctor.AddRange(doctorRepositoryAdo.Get());
            var result = doctor[5];

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
            var doctor = new List<Doctor>();
            var doctorRepositoryAdo = new DoctorRepositoryAdo(Config.ConnectionString);

            // Act
            await doctorRepositoryAdo.Update(DoctorData);
            doctor.AddRange(doctorRepositoryAdo.Get());

            // Assert
            Assert.AreEqual(doctor[2].LastName, DoctorData.LastName);
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
        /// initial data for DoctorData
        /// </summary>
        private const int _id = 3;
       
        /// <summary>
        /// initial data
        /// </summary>
        public static Doctor DoctorData
        {
            get
            {
                return new Doctor() { Id = _id, FirstName = "TestName", Patronymic = "TestPatronymic", LastName = "TestLastName", NumberPhone = "TestNumberPhone" };
            }
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
