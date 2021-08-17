
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


        [SetUp]
        public void Stert()
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
            Assert.NotNull(result);
        }

        [Test]
        public void MethodCreateEntity_ParamsObjRegistrationCard_returnInt_1()
        {
            // Arrange

            RegistrationCardRepositoryADO doc = new RegistrationCardRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.CreateEntity(RegistrationCardData);

            // Assert

        }

        [Test]
        public void MethodUpdateEntity_ParamsObjRegistrationCard_returnInt_1()
        {
            // Arrange

            RegistrationCardRepositoryADO doc = new RegistrationCardRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Update(RegistrationCardData);

            // Assert


        }

        [Test]
        public void MethodDelete_ParamsObjRegistrationCard_returnInt_1()
        {
            // Arrange

            RegistrationCardRepositoryADO doc = new RegistrationCardRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Delete(RegistrationCardData);

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
            Assert.NotNull(result);

        }



        public static RegistrationCard RegistrationCardData
        {
            get
            {
                return new RegistrationCard() { Id = 3, PatientId = 3, DoctorId = 3, DiagnosisId = 2, DateAdmission = DateTime.Now };  
            }
        }

        [TearDown]
        public void End()
        {
            config.DropDataBase();
        }
    }
}
