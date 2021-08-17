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
            var result =  doc.Get();

            // Assert
            Assert.NotEmpty(result);
             
        }

        [Test]
        
        public void MethodCreateEntity_ParamsObjDoctor_returnInt_1()
        {
            // Arrange
            
            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            // Act
            var result =  doc.CreateEntity(entity);

            // Assert
            
          
        }

        [Test]
       
        public void MethodUpdateEntity_ParamsObjDoctor_returnInt_1(Doctor entity)
        {
            // Arrange
           
            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            // Act
            var result =  doc.Update(entity);

            // Assert
            
            config.DropDataBase();

        }

        [Test]
        public void MethodGetAllEntityById_ParamsVoid_returnCollectionEntityDoctor()
        {
            // Arrange
           
            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.GetAllEntityBy(el => el.Id == 5);

            // Assert
            Assert.NotEmpty(result);
          

        }

        [Test]
        
        public void MethodDelete_ParamsObjDoctor_returnInt_1(Doctor entity)
        {
            // Arrange
            
            DoctorRepositoryADO doc = new DoctorRepositoryADO(config.ConnectionString);

            //Act
            var result = doc.Delete(DoctorData);

            //Assert
           
            


        }

        public static Doctor DoctorData
        {
            get
            {
                return new Doctor() { Id = 3, FirstName = "TestName", Patronymic = "TestPatronymic", LastName = "TestLastName", NumberPhone = "TestNumberPhone" };
            }
        }

        ~HospitalTestDoctorRepositoryADO()
        {
            config.DropDataBase();
        }

    }
}
