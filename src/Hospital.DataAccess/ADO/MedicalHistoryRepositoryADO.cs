using Hospital.DataAccess.Entity;
using RepositoryADO.InterfaceForRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.ADO
{
    public class MedicalHistoryRepositoryADO : BaseRepositoryADO<MedicalHistory>
    {
        public MedicalHistoryRepositoryADO(string connectionString) : base(connectionString)
        {
        }
        public async override Task CreateEntity(MedicalHistory entity)
        {
            if (entity != null)
            {
                string sqlExpression = $"INSERT INTO MedicalHistorys (Diagnosis) VALUES ('{entity.Diagnosis}')";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                   await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                   
                }
            }
          

        }

        public async override Task Delete(MedicalHistory entity)
        {
            List<MedicalHistory> result = new List<MedicalHistory>();
            string GetEntityById = $"SELECT * FROM MedicalHistorys WHERE Id ={entity.Id}";


            if (entity != null)
            {
                string sqlExpression = $"DELETE FROM MedicalHistorys WHERE Id= {entity.Id}";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand commandforGetAllId = new SqlCommand(GetEntityById, connection);
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

        public  override  IEnumerable<MedicalHistory> Get()
        {
            List<MedicalHistory> result = new List<MedicalHistory>();
            string sqlExpression = "SELECT * FROM MedicalHistorys";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new MedicalHistory() { Id = reader.GetInt32(0), Diagnosis = reader.GetString(1) });
                }
                return result;
            }
        }

        public override IEnumerable<MedicalHistory> GetAllEntityBy(Expression<Func<MedicalHistory, bool>> predicate)
        {
            List<MedicalHistory> result = new List<MedicalHistory>();
            int item;
            string argument = predicate.ToString().Replace("el => (el.Id == ", string.Empty).Replace(")", string.Empty); ;
            var x = int.TryParse(argument, out item);
            string predicateString = predicate.ToString().Replace("el => (el.", string.Empty).Replace(")", string.Empty).Replace("==", "=");
            string sqlExpression = $"SELECT * FROM MedicalHistorys WHERE {predicateString}";


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string GetAllId = "SELECT * FROM MedicalHistorys Id";

                SqlCommand commandforGetAllId = new SqlCommand(GetAllId, connection);
                SqlDataReader readerId = commandforGetAllId.ExecuteReader();
                List<int> Id = new List<int>();
                while (readerId.Read())
                {
                    Id.Add(readerId.GetInt32(0));
                }

                if (Id.Any(el => el == item  && item > 0))
                {

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new MedicalHistory() { Id = reader.GetInt32(0), Diagnosis = reader.GetString(1) });
                    }
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public async override Task Update(MedicalHistory entity)
        {
            if (entity != null)
            {
                string sqlExpression = $"UPDATE MedicalHistorys SET Diagnosis = '{entity.Diagnosis}'";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    string GetAllId = "SELECT * FROM MedicalHistorys Id";

                    SqlCommand commandforGetAllId = new SqlCommand(GetAllId, connection);
                    SqlDataReader readerId = commandforGetAllId.ExecuteReader();
                    List<int> Id = new List<int>();
                    while (readerId.Read())
                    {
                        Id.Add(readerId.GetInt32(0));
                    }

                    if (Id.Any(el => el >= entity.Id && entity.Id > 0))
                    {
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.ExecuteNonQuery();
                     
                    }
                   
                }
            }
          
        }
    }
}
