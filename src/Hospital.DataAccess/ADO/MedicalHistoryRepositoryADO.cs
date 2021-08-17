using DataAccess.Entity;
using Microsoft.Data.SqlClient;
using RepositoryADO.InterfaceForRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryADO.ImplementationRepository
{
    public class MedicalHistoryRepositoryADO : BaseRepositoryADO<MedicalHistory>
    {
        public MedicalHistoryRepositoryADO(string connectionString) : base(connectionString)
        {
        }
        public override int CreateEntity(MedicalHistory entity)
        {
            if (entity != null)
            {
                string sqlExpression = $"INSERT INTO MedicalHistory (Diagnosis) VALUES ('{entity.Diagnosis}')";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    int number = command.ExecuteNonQuery();
                    return number;
                }
            }
            else
            {
                return 0;
            }

        }

        public override int Delete(MedicalHistory entity)
        {
            List<MedicalHistory> result = new List<MedicalHistory>();
            string GetAllId = "SELECT * FROM MedicalHistory Id";


            if (entity != null)
            {
                string sqlExpression = $"DELETE FROM MedicalHistory WHERE Id= {entity.Id}";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

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
                        int number = command.ExecuteNonQuery();
                        return number;
                    }
                    else
                    {
                        return 0;
                    }


                }
            }
            else
            {
                return 0;
            }
        }

        public override IEnumerable<MedicalHistory> Get()
        {
            List<MedicalHistory> result = new List<MedicalHistory>();
            string sqlExpression = "SELECT * FROM MedicalHistory";
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
            string sqlExpression = $"SELECT * FROM MedicalHistory WHERE {predicateString}";


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string GetAllId = "SELECT * FROM MedicalHistory Id";

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

        public override int Update(MedicalHistory entity)
        {
            if (entity != null)
            {
                string sqlExpression = $"UPDATE MedicalHistory SET Diagnosis = '{entity.Diagnosis}'";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string GetAllId = "SELECT * FROM MedicalHistory Id";

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
                        int number = command.ExecuteNonQuery();
                        return number;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
