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
    public class HospitalTestDoctorRepositoryEntityFramework
    {
        /// <summary>
        ///object for database management
        /// </summary>
        DataBaseConfigurationManager Config = new DataBaseConfigurationManager();

        /// <summary>
        /// initial data
        /// </summary>
        public static Doctor DoctorData
        {
            get
            {
                return new Doctor() { FirstName = "TestName", Patronymic = "TestPatronymic", LastName = "TestLastName", NumberPhone = "TestNumberPhone" };
            }
        }

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
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public void Get_WhenGet_ThenGetAllEntity()
        {
            // Arrange
            var arbitraryValueIndex = 4;
            List<Doctor> result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var doctorRepository = new DoctorRepository(context);
                result = doctorRepository.Get().ToList();
            }

            // Assert
            Assert.AreEqual(result[arbitraryValueIndex].LastName, ComparisonList[arbitraryValueIndex].LastName);
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
                await repositorys.doctorRepository.CreateEntity(DoctorData);
                result = await repositorys.SaveChangesAsync();
            }

            //Assert
            Assert.AreEqual(numberOfChanges, result);
        }

        /// <summary>
        /// method to test Create method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public async Task Create_WhenAddingDoctor_ThenDoctorAdd()
        {
            // Arrange
            const int numberOfChanges = 1;
            int result;
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var doctorRepository = new DoctorRepository(context);
                await doctorRepository.CreateEntity(DoctorData);
                result = await doctorRepository.SaveChanges();
            }

            // Assert
            Assert.AreEqual(numberOfChanges, result);
        }

        /// <summary>
        ///  method to test Delete method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Delete_WhenDoctor_ThenDeleteDoctor()
        {
            // Arrange
            const int numberOfChanges = 1;
            int randomValueId = 3;
            int result;
            var doctor = new Doctor() { Id = randomValueId };
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var doctorRepository = new DoctorRepository(context);
                await doctorRepository.Delete(doctor);
                result = await doctorRepository.SaveChanges();
            }

            // Assert
            Assert.AreEqual(numberOfChanges, result);
        }

        /// <summary>
        /// method to test Update method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Update_WhenDoctor_ThenDoctor()
        {
            int randomValueId = 3;
            const int numberOfChanges = 1;
            int result;
            var doctor = new Doctor() { Id = randomValueId,FirstName = DoctorData.FirstName, LastName = DoctorData.LastName,Patronymic = DoctorData.Patronymic, NumberPhone = DoctorData.NumberPhone};
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            var options = optionsBuilder.UseSqlServer(Config.ConnectionString).Options;

            // Act 
            using (var context = new HospitalContext(options))
            {
                var doctorRepository = new DoctorRepository(context);
                await doctorRepository.Update(doctor);
                result = await doctorRepository.SaveChanges();
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
