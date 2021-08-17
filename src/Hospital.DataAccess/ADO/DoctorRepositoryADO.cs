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
    public class DoctorRepositoryADO : BaseRepositoryADO<Doctor>
    {
        public DoctorRepositoryADO(string connectionString) : base(connectionString)
        {
        }
        public override int CreateEntity(Doctor entity)
        {
            if (entity != null)
            {
                string sqlExpression = $"INSERT INTO Doctor (FirstName,Patronymic,LastName,NumberPhone) VALUES ('{entity.FirstName}','{entity.Patronymic}', '{entity.LastName}', '{entity.NumberPhone}')";
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

        public override int Delete(Doctor entity)
        {
            List<Doctor> result = new List<Doctor>();
            string GetAllId = "SELECT * FROM Doctor Id";


            if (entity != null)
            {
                string sqlExpression = $"DELETE FROM Doctor WHERE Id={entity.Id}";
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

        public override IEnumerable<Doctor> Get()
        {
            List<Doctor> result = new List<Doctor>();
            string sqlExpression = "SELECT * FROM Doctor";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                 connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Doctor() { Id = reader.GetInt32(0), FirstName = reader.GetString(3), Patronymic = reader.GetString(1), LastName = reader.GetString(2), NumberPhone = reader.GetString(4) });
                }
                return result;
            }
        }

        public override IEnumerable<Doctor> GetAllEntityBy(Expression<Func<Doctor, bool>> predicate)
        {
            List<Doctor> result = new List<Doctor>();
            int item;
            string argument = predicate.ToString().Replace("el => (el.Id == ", string.Empty).Replace(")", string.Empty); ;
            var x = int.TryParse(argument, out item);
            string predicateString = predicate.ToString().Replace("el => (el.", string.Empty).Replace(")", string.Empty).Replace("==", "=");
            string sqlExpression =  $"SELECT * FROM Doctor WHERE {predicateString}";


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string GetAllId = "SELECT * FROM Doctor Id";

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
                        result.Add(new Doctor() { Id = reader.GetInt32(0), FirstName = reader.GetString(3), Patronymic = reader.GetString(1), LastName = reader.GetString(2), NumberPhone = reader.GetString(4) });
                    }
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public override int Update(Doctor entity)
        {
            if (entity != null)
            {
                string sqlExpression = $"UPDATE Doctor SET FirstName = '{entity.FirstName}',Patronymic = '{entity.Patronymic}',LastName = '{entity.LastName}',NumberPhone = '{entity.NumberPhone}' WHERE Id={entity.Id}";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string GetAllId = "SELECT * FROM Doctor Id";

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
