using DataAccess.Entity;
using RepositoryADO.InterfaceForRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryADO.ImplementationRepository
{
    public class PatientRepositoryADO : BaseRepositoryADO<Patient>
    {
        public PatientRepositoryADO(string connectionString) : base(connectionString)
        {
        }
        public async override Task CreateEntity(Patient entity)
        {
            if (entity != null)
            {
                string sqlExpression = $"INSERT INTO Patient (FirstName,Patronymic,LastName,Gender,ResidenceAddress) VALUES ('{entity.FirstName}','{entity.Patronymic}', '{entity.LastName}', '{entity.Gender}','{entity.ResidenceAddress}')";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                   await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                    
                }
            }
           

        }

        public async override Task Delete(Patient entity)
        {
            List<MedicalHistory> result = new List<MedicalHistory>();
            string GetAllId = "SELECT * FROM MedicalHistory Id";


            if (entity != null)
            {
                string sqlExpression = $"DELETE FROM MedicalHistory WHERE Id= {entity.Id}";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand commandforGetAllId = new SqlCommand(GetAllId, connection);
                    SqlDataReader readerId = commandforGetAllId.ExecuteReader();
                    List<int> Id = new List<int>();
                    while (readerId.Read())
                    {
                        Id.Add(readerId.GetInt32(0));
                    }

                    if (Id.Any(el => el == entity.Id && entity.Id > 0))
                    {
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.ExecuteNonQuery();
                       
                    }
                 


                }
            }
            
        }

        public override IEnumerable<Patient> Get()
        {
            List<Patient> result = new List<Patient>();
            string sqlExpression = "SELECT * FROM Patient";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Patient() { Id = reader.GetInt32(0), FirstName = reader.GetString(2), Patronymic = reader.GetString(3), LastName = reader.GetString(1), Gender = reader.GetString(4), ResidenceAddress = reader.GetString(5) });
                }
                return result;
            }
        }

        public override IEnumerable<Patient> GetAllEntityBy(Expression<Func<Patient, bool>> predicate)
        {
            List<Patient> result = new List<Patient>();
            int item;
            string argument = predicate.ToString().Replace("el => (el.Id == ", string.Empty).Replace(")", string.Empty); ;
            var x = int.TryParse(argument, out item);
            string predicateString = predicate.ToString().Replace("el => (el.", string.Empty).Replace(")", string.Empty).Replace("==", "=");
            string sqlExpression = $"SELECT * FROM Patient WHERE {predicateString}";


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string GetAllId = "SELECT * FROM Patient Id";

                SqlCommand commandforGetAllId = new SqlCommand(GetAllId, connection);
                SqlDataReader readerId = commandforGetAllId.ExecuteReader();
                List<int> Id = new List<int>();
                while (readerId.Read())
                {
                    Id.Add(readerId.GetInt32(0));
                }

                if (Id.Any(el => el == item && item > 0))
                {

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new Patient() { Id = reader.GetInt32(0), FirstName = reader.GetString(2), Patronymic = reader.GetString(3), LastName = reader.GetString(1), Gender = reader.GetString(4), ResidenceAddress = reader.GetString(5) });
                    }
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public async override Task Update(Patient entity)
        {
            if (entity != null)
            {
                string sqlExpression = $"UPDATE Patient SET FirstName = '{entity.FirstName}',Patronymic = '{entity.Patronymic}',LastName = '{entity.LastName}',Gender = '{entity.Gender}',ResidenceAddress = '{entity.ResidenceAddress}' WHERE Id={entity.Id}";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string GetAllId = "SELECT * FROM Patient Id";
                    await connection.OpenAsync();
                    SqlCommand commandforGetAllId = new SqlCommand(GetAllId, connection);
                    SqlDataReader readerId = commandforGetAllId.ExecuteReader();
                    List<int> Id = new List<int>();
                    while (readerId.Read())
                    {
                        Id.Add(readerId.GetInt32(0));
                    }

                    if (Id.Any(el => el > entity.Id && entity.Id > 0))
                    {
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                         command.ExecuteNonQuery();
                        
                    }
                   
                }
            }
          
        }
    }
}
