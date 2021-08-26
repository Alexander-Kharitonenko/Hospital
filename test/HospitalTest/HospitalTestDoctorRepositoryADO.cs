using Hospital.DataAccess;
using Hospital.DataAccess.ADO;
using Hospital.DataAccess.Entity;
using Hospital.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;


namespace Hospital.XUnitTest
{
    public class HospitalTestDoctorRepositoryADO
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
            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            // Act
            IEnumerable<Doctor> result = doc.Get();

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
            List<Doctor> doctor = new List<Doctor>();
            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            // Act
            await doc.CreateEntity(DoctorData);
            IEnumerable<Doctor> result = doc.GetAllEntityBy(el => el.Id == 6);
            foreach (var i in result)
            {
                doctor.Add(i);
            }

            // Assert
            Assert.AreEqual(doctor[0].LastName, DoctorData.LastName);
        }

        /// <summary>
        /// method to test Update method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task UpdateDoctor_WhenDoctorApdates_ThenDoctorUpdated()
        {
            // Arrange
            List<Doctor> doctor = new List<Doctor>();
            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            // Act
            await doc.Update(DoctorData);
            IEnumerable<Doctor> result = doc.GetAllEntityBy(el => el.Id == 3);
            foreach (var i in result)
            {
                doctor.Add(i);
            }

            // Assert
            Assert.AreEqual(doctor[0].LastName, DoctorData.LastName);
        }

        /// <summary>
        /// method to test GetBy method
        /// </summary>
        /// <returns>IEnumerable<Doctor></returns>
        [Test]
        public void GetAllEntityById_WhenId_5_ThenReturnDoctorWhisId_5()
        {
            // Arrange
            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            // Act
            IEnumerable<Doctor> result = doc.GetAllEntityBy(el => el.Id == 5);

            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        ///  method to test Delete method
        /// </summary>
        /// <returns>void</returns>
        [Test]
        public async Task Delete_WhenId_Doctor_3_ThenDeleteDoctor()
        {
            // Arrange       
            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            //Act
            await doc.Delete(DoctorData);
            var result = doc.GetAllEntityBy(el => el.Id == 3);

            //Assert
            Assert.IsNull(result);
        }

        /// <summary>
        /// initial data
        /// </summary>
        public static Doctor DoctorData
        {
            get
            {
                return new Doctor() { Id = 3, FirstName = "TestName", Patronymic = "TestPatronymic", LastName = "TestLastName", NumberPhone = "TestNumberPhone" };
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
