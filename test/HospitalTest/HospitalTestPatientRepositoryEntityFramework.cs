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
    public class HospitalTestPatientRepositoryEntityFramework
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
                return new Patient() { FirstName = "TestFirstName", Patronymic = "TestPatronymic", LastName = "TestLastName", Gender = "TestGender", ResidenceAddress = "TestResidenceAddress" };
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
        /// method to test Get method
        /// </summary>
        /// <returns>IEnumerable<Patient></returns>
        [Test]
        public void Get_WhenGet_ThenGetAllEntity()
        {
            // Arrange
            const int ARBITRARY_VALUE_INDEX = 4;
            List<Patient> result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var patientRepository = new PatientRepository(context);
                result = patientRepository.Get().ToList();
            }

            // Assert
            Assert.AreEqual(result[ARBITRARY_VALUE_INDEX].LastName, ComparisonList[ARBITRARY_VALUE_INDEX].LastName);
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
            const int NUMBER_OF_CHANGES = 1;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            //Act
            using (var context = new HospitalContext(options))
            {
                var repositorys = new UnitOfWork(new DoctorRepository(context), new MedicalHistoryRepository(context), new PatientRepository(context), new RegistrationCardRepository(context), context);
                await repositorys.patientRepository.CreateEntity(PatientData);
                result = await repositorys.SaveChangesAsync();
            }

            //Assert
            Assert.AreEqual(NUMBER_OF_CHANGES, result);
        }

        /// <summary>
        /// method to test Create method
        /// </summary>
        /// <returns>IEnumerable<Patient></returns>
        [Test]
        public async Task Create_WhenAddingPatient_ThenPatientAdd()
        {
            // Arrange
            const int NUMBER_OF_CHANGES = 1;
            int result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var patientRepository = new PatientRepository(context);
                await patientRepository.CreateEntity(PatientData);
                result = await patientRepository.SaveChanges();
            }

            // Assert
            Assert.AreEqual(NUMBER_OF_CHANGES, result);
        }

        /// <summary>
        ///  method to test Delete method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Delete_WhenPatient_ThenDeletePatient()
        {
            // Arrange
            const int NUMBER_OF_CHANGES = 1;
            int randomValueId = 3;
            int result;
            var patient = new Patient() { Id = randomValueId };
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var patientRepository = new PatientRepository(context);
                await patientRepository.Delete(patient);
                result = await patientRepository.SaveChanges();
            }

            // Assert
            Assert.AreEqual(NUMBER_OF_CHANGES, result);
        }

        /// <summary>
        /// method to test Update method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Update_WhenPatient_ThenPatient()
        {
            int randomValueId = 3;
            const int NUMBER_OF_CHANGES = 1;
            int result;
            var patient = new Patient() { Id = randomValueId,FirstName = PatientData.FirstName, LastName = PatientData.LastName,Patronymic = PatientData.Patronymic,Gender = PatientData.Gender, ResidenceAddress = PatientData.ResidenceAddress};
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var patientRepository = new PatientRepository(context);
                await patientRepository.Update(patient);
                result = await patientRepository.SaveChanges();
            }

            // Assert
            Assert.AreEqual(NUMBER_OF_CHANGES, result);
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
