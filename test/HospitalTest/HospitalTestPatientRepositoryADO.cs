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
        public void Get_WhenGet_HenGetAllPatient()
        {
            // Arrange

            PatientRepositoryADO doc = new PatientRepositoryADO(config.ConnectionString);

            // Act
            IEnumerable<Patient> result = doc.Get();

            // Assert
            Assert.NotNull(result);

        }

        [Test]
        public async Task CreateEntity_WhenCreatePatient_ThenCreatePatient()
        {
            // Arrange
            List<Patient> patients = new List<Patient>();
            PatientRepositoryADO doc = new PatientRepositoryADO(config.ConnectionString);

            // Act
            await doc.CreateEntity(PatientData);
            var result = doc.GetAllEntityBy(el => el.Id == 6);
            foreach (var i in result)
            {
                patients.Add(i);
            }

            // Assert
            Assert.AreEqual(patients[0].LastName, PatientData.LastName);
        }

        [Test]
        public async Task UpdateEntity_WhenPatient_ThenUpdatePatient()
        {
            // Arrange
            List<Patient> patients = new List<Patient>();
            PatientRepositoryADO doc = new PatientRepositoryADO(config.ConnectionString);

            // Act
            await doc.Update(PatientData);
            var result = doc.GetAllEntityBy(el => el.Id == 3);
            foreach (var i in result)
            {
                patients.Add(i);
            }
            // Assert
            Assert.AreEqual(patients[0].LastName, PatientData.LastName);

        }

        [Test]
       
        public async Task Delete_WhenPatient_ThenDeletePatient()
        {
            // Arrange
            List<Patient> patients = new List<Patient>();
            PatientRepositoryADO doc = new PatientRepositoryADO(config.ConnectionString);

            // Act
            await doc.Delete(PatientData);
            var result = doc.GetAllEntityBy(el => el.Id == 3);


            // Assert
            Assert.IsNull(result);
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
