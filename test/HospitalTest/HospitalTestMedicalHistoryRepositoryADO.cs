using DataAccess.Entity;
using NUnit.Framework;
using RepositoryADO.ImplementationRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;

namespace Hospital.XUnitTest
{
   
   
    public class HospitalTestMedicalHistoryRepositoryADO
    {
        DataBaseConfigurationManager config = new DataBaseConfigurationManager();

        [SetUp]
        public void Stert()
        {
            config.LoadDataBase();
        }

        [Test]
        public void MethodGet_ParamsVoid_returnCollectionEntityMedicalHistory()
        {
            // Arrange

            MedicalHistoryRepositoryADO doc = new MedicalHistoryRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Get();

            // Assert
            Assert.IsNotNull(result);

        }

        [Test]
       
        public void MethodCreateEntity_ParamsObjMedicalHistory_returnInt_1()
        {
            // Arrange

            MedicalHistoryRepositoryADO doc = new MedicalHistoryRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.CreateEntity(MedicalHistoryData);

            // Assert

        }

        [Test]   
        public void MethodUpdateEntity_ParamsObjMedicalHistory_returnInt_1()
        {
            // Arrange

            MedicalHistoryRepositoryADO doc = new MedicalHistoryRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Update(MedicalHistoryData);

            // Assert



        }

        [Test]
        public void MethodDelete_ParamsObjMedicalHistory_returnInt_1()
        {
            // Arrange

            MedicalHistoryRepositoryADO doc = new MedicalHistoryRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Delete(MedicalHistoryData);

            // Assert

        }

        [Test]
        public void MethodGetAllEntityById_ParamsVoid_returnCollectionEntityMedicalHistory()
        {
            // Arrange

            MedicalHistoryRepositoryADO doc = new MedicalHistoryRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.GetAllEntityBy(el => el.Id == 5);

            // Assert
            Assert.IsNotNull(result);
        }



        public static MedicalHistory MedicalHistoryData
        {
            get
            {
                return new MedicalHistory() { Id = 3, Diagnosis = "TestDiagnosis" };

            }
        }

        [TearDown]
        public void End()
        {
            config.DropDataBase();
        }

    }
}
