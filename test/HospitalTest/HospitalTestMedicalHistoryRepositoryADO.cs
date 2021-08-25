using Hospital.DataAccess.ADO;
using Hospital.DataAccess.Entity;
using NUnit.Framework;
using System;
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
            var result =  doc.GetAllEntityBy(el => el.Id == 6);
            foreach (var i in result)
            {
                histors.Add(i);
            }

            // Assert
            Assert.AreEqual(histors[0].Diagnosis, MedicalHistoryData.Diagnosis);
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
            var result = doc.GetAllEntityBy(el => el.Id == 3);
            foreach (var i in result)
            {
                histors.Add(i);
            }

            // Assert
            Assert.AreEqual(histors[0].Diagnosis, MedicalHistoryData.Diagnosis);
        }

        /// <summary>
        ///  method to test Delete method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Delete_WhenMedicalHistory_ThenDeleteMedicalHistory()
        {
            // Arrange
            var histors = new List<MedicalHistory>();
            var doc = new MedicalHistoryRepositoryAdo(config.ConnectionString);

            // Act
            await doc.Delete(MedicalHistoryData);
            var result = doc.GetAllEntityBy(el => el.Id == 3);
            
            // Assert
            Assert.IsNull(result);
        }

        
        ///  /// <summary>
        /// method to test GetBy method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public void GetAllEntityById_WhenId_5_ThenReturnMedicalHistoryWhithId_5()
        {
            // Arrange
            var doc = new MedicalHistoryRepositoryAdo(config.ConnectionString);

            // Act
            var result = doc.GetAllEntityBy(el => el.Id == 5);

            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// initial data
        /// </summary>
        public static MedicalHistory MedicalHistoryData
        {
            get
            {
                return new MedicalHistory() { Id = 3, Diagnosis = "TestDiagnosis" };
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
