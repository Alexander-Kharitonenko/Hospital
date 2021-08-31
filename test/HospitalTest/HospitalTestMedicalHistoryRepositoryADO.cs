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
    /// class for test MedicalHistoryRepositoryAdo
    /// </summary>
    public class HospitalTestMedicalHistoryRepositoryAdo
    {
        /// <summary>
        ///object for database management
        /// </summary>
        DataBaseConfigurationManager Config = new DataBaseConfigurationManager();

        /// <summary>
        /// list with reference values ​​for comparison
        /// </summary>
        List<MedicalHistory> ComparisonList = new List<MedicalHistory>()
        {
                new MedicalHistory() { Id = 1,  Diagnosis ="Stroke"},
                new MedicalHistory() { Id = 2,  Diagnosis ="Diabetes"},
                new MedicalHistory() { Id = 3,  Diagnosis ="Tuberculosis"},
                new MedicalHistory() { Id = 4, Diagnosis ="AIDS"},
                new MedicalHistory() { Id = 5,  Diagnosis ="Brain cancer"},
        };

        /// <summary>
        /// initial data for MedicalHistoryData
        /// </summary>
        private const int ARBITRARY_VALUE_ID = 3;

        /// <summary>
        /// initial data
        /// </summary>
        public static MedicalHistory MedicalHistoryData
        {
            get
            {
                return new MedicalHistory() { Id = ARBITRARY_VALUE_ID, Diagnosis = "TestDiagnosis" };
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
        public void Get_WhenGet_ThenReturnMedicalHistory()
        {
            // Arrange
            var arbitraryValueIndex = 4;
            var medicalHistoryRepositoryAdo = new MedicalHistoryRepositoryAdo(Config.ConnectionString);

            // Act
            var result = medicalHistoryRepositoryAdo.Get().ToList();

            // Assert
            Assert.AreEqual(result[arbitraryValueIndex].Diagnosis, ComparisonList[arbitraryValueIndex].Diagnosis);
        }

        /// <summary>
        /// method to test Create method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public async Task CreateEntity_WhenMedicalHistory_ThenCreateMedicalHistory()
        {
            // Arrange
            var arbitraryValueIndex = 5;
            var histors = new List<MedicalHistory>();
            var medicalHistoryRepositoryAdo = new MedicalHistoryRepositoryAdo(Config.ConnectionString);

            // Act
            await medicalHistoryRepositoryAdo.CreateEntity(MedicalHistoryData);
            histors.AddRange(medicalHistoryRepositoryAdo.Get());
            var result = histors[arbitraryValueIndex];

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
            var arbitraryValueIndex = 2;
            var histors = new List<MedicalHistory>();
            var medicalHistoryRepositoryAdo = new MedicalHistoryRepositoryAdo(Config.ConnectionString);

            // Act
            await medicalHistoryRepositoryAdo.Update(MedicalHistoryData);
            histors.AddRange(medicalHistoryRepositoryAdo.Get());

            // Assert
            Assert.AreEqual(histors[arbitraryValueIndex].Diagnosis, MedicalHistoryData.Diagnosis);
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
            var medicalHistoryRepositoryAdo = new MedicalHistoryRepositoryAdo(Config.ConnectionString);

            // Act
            await medicalHistoryRepositoryAdo.Delete(MedicalHistoryData);
            histors.AddRange(medicalHistoryRepositoryAdo.Get());

            //Assert
            Assert.AreEqual(allRecordsAfterDeletion, histors.Count);
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
