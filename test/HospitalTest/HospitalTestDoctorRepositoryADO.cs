using Hospital.DataAccess.ADO;
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
        public void Get_WhenGetDoctor_ThenGetDoctor()
        {
            // Arrange
            var doc = new DoctorRepositoryAdo(config.ConnectionString);

            // Act
            var result = doc.Get();

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
            var doc = new DoctorRepositoryAdo(config.ConnectionString);

            // Act
            await doc.CreateEntity(DoctorData);
            doctor.AddRange(doc.Get());
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
            var doc = new DoctorRepositoryAdo(config.ConnectionString);

            // Act
            await doc.Update(DoctorData);
            doctor.AddRange(doc.Get());

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
            var doc = new DoctorRepositoryAdo(config.ConnectionString);

            //Act    
            await doc.Delete(DoctorData);
            doctor.AddRange(doc.Get());

            //Assert
            Assert.AreEqual(allRecordsAfterDeletion, doctor.Count);
        }

        /// <summary>
        /// initial data for DoctorData
        /// </summary>
        private const int Id = 3;
       
        /// <summary>
        /// initial data
        /// </summary>
        public static Doctor DoctorData
        {
            get
            {
                return new Doctor() { Id = Id, FirstName = "TestName", Patronymic = "TestPatronymic", LastName = "TestLastName", NumberPhone = "TestNumberPhone" };
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
