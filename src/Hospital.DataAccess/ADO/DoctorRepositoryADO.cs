using Hospital.DataAccess.Entity;
using RepositoryADO.InterfaceForRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hospital.DataAccess.ADO
{
    public class DoctorRepositoryADO : BaseRepositoryADO<Doctor>
    {
        public DoctorRepositoryADO(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// add a new object to the database
        /// </summary>
        /// <param name="entity">object to add</param>
        /// <returns>void</returns>
        public async override Task CreateEntity(Doctor entity)
        {
            if (entity != null)
            {
                string sqlExpression = $"INSERT INTO Doctors (FirstName,Patronymic,LastName,NumberPhone) VALUES ('{entity.FirstName}','{entity.Patronymic}', '{entity.LastName}', '{entity.NumberPhone}')";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Delete a object to the database
        /// </summary>
        /// <param name="entity">object to Delete</param>
        /// <returns>void</returns>
        public async override Task Delete(Doctor entity)
        {
            List<Doctor> result = new List<Doctor>();
            string GetEntityById = $"SELECT * FROM Doctors WHERE Id ={entity.Id}";

            if (entity != null)
            {
                string sqlExpression = $"DELETE FROM Doctors WHERE Id={entity.Id}";
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

        /// <summary>
        /// get all object from database
        /// </summary>
        /// <returns>void</returns>
        public override IEnumerable<Doctor> Get()
        {
            List<Doctor> result = new List<Doctor>();
            string sqlExpression = "SELECT * FROM Doctors";
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

        /// <summary>
        /// returns all elements that match a condition
        /// </summary>
        /// <param name="predicate">condition</param>
        /// <returns>IEnumerable<Doctor></returns>
        public override IEnumerable<Doctor> GetAllEntityBy(Expression<Func<Doctor, bool>> predicate)
        {
            List<Doctor> result = new List<Doctor>();
            int item;
            string argument = predicate.ToString().Replace("el => (el.Id == ", string.Empty).Replace(")", string.Empty); ;
            var x = int.TryParse(argument, out item);
            string predicateString = predicate.ToString().Replace("el => (el.", string.Empty).Replace(")", string.Empty).Replace("==", "=");
            string sqlExpression = $"SELECT * FROM Doctors WHERE {predicateString}";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string GetAllId = "SELECT * FROM Doctors Id";
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
                        result.Add(new Doctor() { Id = reader.GetInt32(0), FirstName = reader.GetString(2), Patronymic = reader.GetString(3), LastName = reader.GetString(1), NumberPhone = reader.GetString(4) });
                    }
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Update a object to the database
        /// </summary>
        /// <param name="entity">object to Update</param>
        /// <returns>void</returns>
        public async override Task Update(Doctor entity)
        {
            if (entity != null)
            {
                string sqlExpression = $"UPDATE Doctors SET FirstName = '{entity.FirstName}',Patronymic = '{entity.Patronymic}',LastName = '{entity.LastName}',NumberPhone = '{entity.NumberPhone}' WHERE Id={entity.Id}";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    string GetAllId = "SELECT * FROM Doctors Id";

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
