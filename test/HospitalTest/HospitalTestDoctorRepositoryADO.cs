using DataAccess.Entity;
using Hospital.DataAccess;
using Hospital.DataAccess.ADO;
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
        DataBaseConfigurationManager config = new DataBaseConfigurationManager();
        
        

        [SetUp]
        public void Stert()
        {
            config.LoadDataBase();
        }

        [Test]
        public void Get_WhenGetDoctor_ThenReturnDoctor()
        {
            // Arrange
            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);
            
            // Act
            var result = doc.Get();

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task CreateEntity_WhenAddingDoctor_ThenDoctorAdded()
        {
            // Arrange
            List<Doctor> doctor = new List<Doctor>();
            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);
            
            // Act
            await doc.CreateEntity(DoctorData);
            IEnumerable<Doctor> result = doc.GetAllEntityBy(el => el.Id == 6);
            foreach(var i in result) 
            {
                doctor.Add(i);
            }
         
            // Assert
            Assert.AreEqual(doctor[0].LastName, DoctorData.LastName);
        }

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

        [Test]
        public void GetAllEntityById_WenId_5_ThenReturnDoctorWhisId_5()
        {
            // Arrange
            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.GetAllEntityBy(el => el.Id == 5);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Delete_WenId_Doctor_3_ThenDeleteDoctor()
        {
            // Arrange       
            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);
           
            //Act
             await doc.Delete(DoctorData);
             var result = doc.GetAllEntityBy(el => el.Id == 3);

            //Assert
            Assert.IsNull(result);
        }

        public static Doctor DoctorData
        {
            get
            {
                return new Doctor() { FirstName = "TestName", Patronymic = "TestPatronymic", LastName = "TestLastName", NumberPhone = "TestNumberPhone" };
            }
        }

        [Test]
        public async Task CreateEFCore_WhenAddingDoctor_ThenDoctorAdded()
        {
            // Arrange
            int reulr;
            List<Doctor> doctor = new List<Doctor>();
            var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
            DbContextOptions<HospitalContext> options = optionsBuilder.UseSqlServer(config.ConnectionString).Options;

            // Act
            using (HospitalContext ct = new HospitalContext(options)) 
            {
                DoctorRepository doc = new DoctorRepository(ct);
                await doc.CreateEntity(DoctorData);
                reulr = await doc.SaveChanges();

            }
            
            // Assert
            Assert.AreEqual(reulr, reulr);
        }

        [TearDown]
        public void End()
        {
            config.DropDataBase();
        }

    }
}
