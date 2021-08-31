using Hospital.DataAccess;
using Hospital.DataAccess.Entity;
using Hospital.DataAccess.RepositoryEntityFramework;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;

namespace HospitalTest
{
    /// <summary>
    /// class for test RepositoryEF
    /// </summary>
    public class HospitalTestMedicalHistoryRepositoryEntityFramework
    {
        /// <summary>
        ///object for database management
        /// </summary>
        DataBaseConfigurationManager Config = new DataBaseConfigurationManager();

        /// <summary>
        /// initial data
        /// </summary>
        public static MedicalHistory MedicalHistoryData
        {
            get
            {
                return new MedicalHistory() { Diagnosis = "TestDiagnosis" };
            }
        }

        /// <summary>
        /// list with reference values ​​for comparison
        /// </summary>
        List<MedicalHistory> ComparisonList = new List<MedicalHistory>()
        {
                new MedicalHistory() { Id = 1, Diagnosis="Stroke"},
                new MedicalHistory() { Id = 2, Diagnosis= "Diabetes"},
                new MedicalHistory() { Id = 3, Diagnosis="Tuberculosis"},
                new MedicalHistory() { Id = 4, Diagnosis="AIDS"},
                new MedicalHistory() { Id = 5, Diagnosis="Brain cancer"},
        };

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
        /// method to test Get method
        /// </summary>
        /// <returns>IEnumerable<MedicalHistory></returns>
        [Test]
        public void Get_WhenGet_ThenGetAllEntity()
        {
            // Arrange
            var arbitraryValueIndex = 4;
            List<MedicalHistory> result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var doctorRepository = new MedicalHistoryRepository(context);
                result = doctorRepository.Get().ToList();
            }

            // Assert
            Assert.AreEqual(result[arbitraryValueIndex].Diagnosis, ComparisonList[arbitraryValueIndex].Diagnosis);
        }

        /// <summary>
        ///  method to Save Changes
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task SaveChanges_WenAddEntity_ThenSaveChangesEntity()
        {
            // Arrange
            int result;
            const int numberOfChanges = 1;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            //Act
            using (var context = new HospitalContext(options))
            {
                var repositorys = new UnitOfWork(new DoctorRepository(context), new MedicalHistoryRepository(context), new PatientRepository(context), new RegistrationCardRepository(context), context);
                await repositorys.medicalHistoryRepository.CreateEntity(MedicalHistoryData);
                result = await repositorys.SaveChangesAsync();
            }

            //Assert
            Assert.AreEqual(numberOfChanges, result);
        }

        /// <summary>
        /// method to test Create method
        /// </summary>
        /// <returns>IEnumerable<MedicalHistory></returns>
        [Test]
        public async Task Create_WhenAddingMedicalHistory_ThenMedicalHistoryAdd()
        {
            // Arrange
            const int numberOfChanges = 1;
            int result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var medicalHistoryRepository = new MedicalHistoryRepository(context);
                await medicalHistoryRepository.CreateEntity(MedicalHistoryData);
                result = await medicalHistoryRepository.SaveChanges();
            }

            // Assert
            Assert.AreEqual(numberOfChanges, result);
        }

        /// <summary>
        ///  method to test Delete method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Delete_WhenMedicalHistory_ThenDeleteMedicalHistory()
        {
            // Arrange
            const int numberOfChanges = 1;
            int randomValueId = 3;
            int result;
            var medicalHistory = new MedicalHistory() { Id = randomValueId };
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var medicalHistoryRepository = new MedicalHistoryRepository(context);
                await medicalHistoryRepository.Delete(medicalHistory);
                result = await medicalHistoryRepository.SaveChanges();
            }

            // Assert
            Assert.AreEqual(numberOfChanges, result);
        }

        /// <summary>
        /// method to test Update method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Update_WhenMedicalHistory_ThenMedicalHistory()
        {
            int randomValueId = 3;
            const int numberOfChanges = 1;
            int result;
            var medicalHistory = new MedicalHistory() { Id = randomValueId, Diagnosis="TestDiagnisis"};
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var medicalHistoryRepository = new MedicalHistoryRepository(context);
                await medicalHistoryRepository.Update(medicalHistory);
                result = await medicalHistoryRepository.SaveChanges();
            }

            // Assert
            Assert.AreEqual(numberOfChanges, result);
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
