
using DataAccess.Entity;
using NUnit.Framework;
using RepositoryADO.ImplementationRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;

namespace Hospital.XUnitTest
{
    public class HospitalTestRegistrationCardRepositoryADO
    {

        DataBaseConfigurationManager config = new DataBaseConfigurationManager();
        

        public HospitalTestRegistrationCardRepositoryADO() 
        {
            config.LoadDataBase();
        }

        [Test]
        public void MethodGet_ParamsVoid_returnCollectionEntityRegistrationCard()
        {
            // Arrange
            
            RegistrationCardRepositoryADO doc = new RegistrationCardRepositoryADO(config.ConnectionString);

            // Act
            IEnumerable<RegistrationCard> result = doc.Get();

            // Assert
          
        }

        [Test]
        [MemberData(nameof(RegistrationCardData))]
        public void MethodCreateEntity_ParamsObjRegistrationCard_returnInt_1(RegistrationCard entity)
        {
            // Arrange
     
            RegistrationCardRepositoryADO doc = new RegistrationCardRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.CreateEntity(entity);

            // Assert
           
        }

        [Test]
        [MemberData(nameof(RegistrationCardData))]
        public void MethodUpdateEntity_ParamsObjRegistrationCard_returnInt_1(RegistrationCard entity)
        {
            // Arrange
           
            RegistrationCardRepositoryADO doc = new RegistrationCardRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Update(entity);

            // Assert
          

        }

        [Test]
        [MemberData(nameof(RegistrationCardData))]
        public void MethodDelete_ParamsObjRegistrationCard_returnInt_1(RegistrationCard entity)
        {
            // Arrange
           
            RegistrationCardRepositoryADO doc = new RegistrationCardRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Delete(entity);

            // Assert
           
        }

        [Test]
        public void MethodGetAllEntityById_ParamsVoid_returnCollectionEntityRegistrationCard()
        {
            // Arrange
            
            RegistrationCardRepositoryADO doc = new RegistrationCardRepositoryADO(config.ConnectionString);

            // Act
            IEnumerable<RegistrationCard> result = doc.GetAllEntityBy(el => el.Id == 5);

            // Assert
           
        }



        public static IEnumerable<object[]> RegistrationCardData
        {
            get
            {
                return new List<object[]>
                {
                  new object[] {  new RegistrationCard() { Id = 3, PatientId = 3 , DoctorId = 3 , DiagnosisId = 2 , DateAdmission = DateTime.Now } }
                };
            }
        }

        ~HospitalTestRegistrationCardRepositoryADO()
        {
            config.LoadDataBase();
        }
    }
}
