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
       
        public HospitalTestPatientRepositoryADO() 
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
            Assert.NotEmpty(result);
          
        }

        [Test]
        [MemberData(nameof(PatientData))]
        public void MethodCreateEntity_ParamsObjPatient_returnInt_1(Patient entity)
        {
            // Arrange
           
            PatientRepositoryADO doc = new PatientRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.CreateEntity(entity);

            // Assert
            
        }

        [Test]
        [MemberData(nameof(PatientData))]
        public void MethodUpdateEntity_ParamsObjPatient_returnInt_1(Patient entity)
        {
            // Arrange
         
            PatientRepositoryADO doc = new PatientRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Update(entity);

            // Assert
           

        }

        [Test]
        [MemberData(nameof(PatientData))]
        public void MethodDelete_ParamsObjPatient_returnInt_1(Patient entity)
        {
            // Arrange
         
            PatientRepositoryADO doc = new PatientRepositoryADO(config.ConnectionString);

            // Act
            var result = doc.Delete(entity);

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
           
        }



        public static IEnumerable<object[]> PatientData
        {
            get
            {
                return new List<object[]>
                {
                  new object[] {  new Patient() { Id = 3, FirstName = "TestFirstName", Patronymic= "TestPatronymic", LastName= "TestLastName", Gender= "TestGender", ResidenceAddress= "TestResidenceAddress" } }
                };
            }
        }

        ~HospitalTestPatientRepositoryADO()
        {
            config.DropDataBase();
        }


    }
}
