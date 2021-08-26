using Hospital.DataAccess.ADO;
using Hospital.DataAccess.Entity;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;

namespace Hospital.XUnitTest
{
    /// <summary>
    /// class for test MedicalHistoryRepositoryAdo
    /// </summary>
    public class HospitalTestMedicalHistoryRepositoryAdo
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
        public void Get_WhenGet_ThenReturnMedicalHistory()
        {
            // Arrange
            var doc = new MedicalHistoryRepositoryAdo(config.ConnectionString);

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
        public async Task CreateEntity_WhenMedicalHistory_ThenCreateMedicalHistory()
        {
            // Arrange
            var histors = new List<MedicalHistory>();
            var doc = new MedicalHistoryRepositoryAdo(config.ConnectionString);

            // Act
            await doc.CreateEntity(MedicalHistoryData);
            histors.AddRange(doc.Get());
            var result = histors[5];

            // Assert
            Assert.AreEqual(result.Diagnosis, MedicalHistoryData.Diagnosis);
        }

        /// <summary>
        /// method to test Update method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Update_WhenMedicalHistory_ThenUpdateMedicalHistory()
        {
            // Arrange
            var histors = new List<MedicalHistory>();
            var doc = new MedicalHistoryRepositoryAdo(config.ConnectionString);

            // Act
            await doc.Update(MedicalHistoryData);
            histors.AddRange(doc.Get());

            // Assert
            Assert.AreEqual(histors[2].Diagnosis, MedicalHistoryData.Diagnosis);
        }

        /// <summary>
        ///  method to test Delete method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Delete_WhenMedicalHistory_ThenDeleteMedicalHistory()
        {
            // Arrange
            const int allRecordsAfterDeletion = 4;
            var histors = new List<MedicalHistory>();
            var doc = new MedicalHistoryRepositoryAdo(config.ConnectionString);

            // Act
            await doc.Delete(MedicalHistoryData);
            histors.AddRange(doc.Get());

            //Assert
            Assert.AreEqual(allRecordsAfterDeletion, histors.Count);
        }

        /// <summary>
        /// initial data for MedicalHistoryData
        /// </summary>
        private const int Id = 3;

        /// <summary>
        /// initial data
        /// </summary>
        public static MedicalHistory MedicalHistoryData
        {
            get
            {
                return new MedicalHistory() { Id = Id, Diagnosis = "TestDiagnosis" };
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
