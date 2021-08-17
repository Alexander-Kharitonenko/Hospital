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
        public HospitalTestMedicalHistoryRepositoryADO() 
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
            Assert.NotEmpty(result);
            
        }

        [Test]
        [MemberData(nameof(MedicalHistoryData))]
        public void MethodCreateEntity_ParamsObjMedicalHistory_returnInt_1(MedicalHistory entity)
        {
            // Arrange
           
            MedicalHistoryRepositoryADO doc = new MedicalHistoryRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.CreateEntity(entity);

            // Assert
           
        }

        [Test]
        [MemberData(nameof(MedicalHistoryData))]
        public void MethodUpdateEntity_ParamsObjMedicalHistory_returnInt_1(MedicalHistory entity)
        {
            // Arrange
          
            MedicalHistoryRepositoryADO doc = new MedicalHistoryRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Update(entity);

            // Assert
           
            

        }

        [Test]
        [MemberData(nameof(MedicalHistoryData))]
        public void MethodDelete_ParamsObjMedicalHistory_returnInt_1(MedicalHistory entity)
        {
            // Arrange
           
            MedicalHistoryRepositoryADO doc = new MedicalHistoryRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Delete(entity);

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
           
        }



        public static IEnumerable<object[]> MedicalHistoryData
        {
            get
            {
                return new List<object[]>
                {
                  new object[] {  new MedicalHistory() { Id = 3, Diagnosis = "TestDiagnosis" } }
                };
            }
        }

        ~HospitalTestMedicalHistoryRepositoryADO()
        {
            config.DropDataBase();
        }

    }
}
