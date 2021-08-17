using DataAccess.Entity;
using NUnit.Framework;
using RepositoryADO.ImplementationRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.IntegrationTests;
namespace Hospital.XUnitTest
{
    public class HospitalTestPatientRepositoryADO
    {
        DataBaseConfigurationManager config = new DataBaseConfigurationManager();

        [SetUp]
        public void Stert()
        {
            config.LoadDataBase();
        }

        [Test]
        public void MethodGet_ParamsVoid_returnCollectionEntityPatient()
        {
            // Arrange

            PatientRepositoryADO doc = new PatientRepositoryADO(config.ConnectionString);

            // Act
            IEnumerable<Patient> result = doc.Get();

            // Assert
            Assert.NotNull(result);

        }

        [Test]
        public void MethodCreateEntity_ParamsObjPatient_returnInt_1()
        {
            // Arrange

            PatientRepositoryADO doc = new PatientRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.CreateEntity(PatientData);

            // Assert

        }

        [Test]
        public void MethodUpdateEntity_ParamsObjPatient_returnInt_1()
        {
            // Arrange

            PatientRepositoryADO doc = new PatientRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Update(PatientData);

            // Assert


        }

        [Test]
       
        public void MethodDelete_ParamsObjPatient_returnInt_1()
        {
            // Arrange

            PatientRepositoryADO doc = new PatientRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Delete(PatientData);

            // Assert

        }

        [Test]
        public void MethodGetAllEntityById_ParamsVoid_returnCollectionEntityPatient()
        {
            // Arrange

            PatientRepositoryADO doc = new PatientRepositoryADO(config.ConnectionString);

            // Act
            IEnumerable<Patient> result = doc.GetAllEntityBy(el => el.Id == 5);

            // Assert

            Assert.NotNull(result);

        }



        public static Patient PatientData
        {
            get
            {
                return new Patient() { Id = 3, FirstName = "TestFirstName", Patronymic = "TestPatronymic", LastName = "TestLastName", Gender = "TestGender", ResidenceAddress = "TestResidenceAddress" };
               
            }
        }

        [TearDown]
        public void End()
        {
            config.DropDataBase();
        }


    }
}
