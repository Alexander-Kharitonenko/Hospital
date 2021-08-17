using DataAccess.Entity;
using Hospital.DataAccess.ADO;
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
        public void MethodGet_ParamsVoid_returnCollectionEntityDoctor()
        {
            // Arrange


            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Get();

            // Assert

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task MethodCreateEntity_ParamsObjDoctor_returnInt_1()
        {
            // Arrange

            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            // Act
            await doc.CreateEntity(DoctorData);

            // Assert


        }

        [Test]
        public async Task MethodUpdateEntity_ParamsObjDoctor_returnInt_1()
        {
            // Arrange

            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            // Act
            await doc.Update(DoctorData);

            // Assert

            

        }

        [Test]
        public void MethodGetAllEntityById_ParamsVoid_returnCollectionEntityDoctor()
        {
            // Arrange

            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.GetAllEntityBy(el => el.Id == 5);

            // Assert
            Assert.IsNotNull(result);


        }

        [Test]
        public async Task  MethodDelete_ParamsObjDoctor_returnInt_1()
        {
            // Arrange

            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            //Act
             await doc.Delete(DoctorData);

            //Assert




        }

        public static Doctor DoctorData
        {
            get
            {
                return new Doctor() { Id = 3, FirstName = "TestName", Patronymic = "TestPatronymic", LastName = "TestLastName", NumberPhone = "TestNumberPhone" };
            }
        }

        [TearDown]
        public void End()
        {
            config.DropDataBase();
        }

    }
}
