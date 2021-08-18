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
        public void Get_WhenGet_ThenReturnMedicalHistory()
        {
            // Arrange
            MedicalHistoryRepositoryADO doc = new MedicalHistoryRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Get();

            // Assert
            Assert.IsNotNull(result);

        }

        [Test]
       
        public async Task CreateEntity_WhenMedicalHistory_ThenCreateMedicalHistory()
        {
            // Arrange
            List<MedicalHistory> histors = new List<MedicalHistory>();
            MedicalHistoryRepositoryADO doc = new MedicalHistoryRepositoryADO(config.ConnectionString);

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

        [Test]   
        public async Task Update_WhenMedicalHistory_ThenUpdateMedicalHistory()
        {
            // Arrange
            List<MedicalHistory> histors = new List<MedicalHistory>();
            MedicalHistoryRepositoryADO doc = new MedicalHistoryRepositoryADO(config.ConnectionString);

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

        [Test]
        public async Task Delete_WhenMedicalHistory_ThenDeleteMedicalHistory()
        {
            // Arrange
            List<MedicalHistory> histors = new List<MedicalHistory>();
            MedicalHistoryRepositoryADO doc = new MedicalHistoryRepositoryADO(config.ConnectionString);

            // Act
            await doc.Delete(MedicalHistoryData);
            var result = doc.GetAllEntityBy(el => el.Id == 3);
            
            // Assert
            Assert.IsNull(result);
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
