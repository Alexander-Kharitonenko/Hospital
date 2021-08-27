using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;
using Hospital.DataAccess.RepositoryAdo;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.DataAccess.RepositoryAdo
{
    /// <summary>
    /// contains the logic for working with DoctorRepository using ADO technology
    /// </summary>
    public class DoctorRepositoryAdo : BaseRepositoryAdo<Doctor>, IDoctorRepository
    {
        /// <summary>
        /// constructor for initializing class fields
        /// </summary>
        /// <param name="connectionString">database connection field</param>
        public DoctorRepositoryAdo(string connectionString) : base(connectionString)
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
                var sqlExpression = $"INSERT INTO Doctors (FirstName,Patronymic,LastName,NumberPhone) VALUES ('{entity.LastName}', '{entity.FirstName}', '{entity.Patronymic}', '{entity.NumberPhone}')";
                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand(sqlExpression, connection);
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
            var result = new List<Doctor>();
            var getEntityById = $"SELECT * FROM Doctors WHERE Id ={entity.Id}";

            if (entity != null)
            {
                var sqlExpression = $"DELETE FROM Doctors WHERE Id={entity.Id}";
                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    var commandforGetAllId = new SqlCommand(getEntityById, connection);
                    var readerId = commandforGetAllId.ExecuteReader();
                    var Id = new List<int>();
                    while (readerId.Read())
                    {
                        Id.Add(readerId.GetInt32(0));
                    }

                    if (Id.Any(el => el == entity.Id && entity.Id > 0))
                    {
                        var command = new SqlCommand(sqlExpression, connection);
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
            var result = new List<Doctor>();
            var sqlExpression = "SELECT * FROM Doctors";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Doctor() { Id = reader.GetInt32(0), FirstName = reader.GetString(3), Patronymic = reader.GetString(1), LastName = reader.GetString(2), NumberPhone = reader.GetString(4) });
                }
                return result;
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
                var sqlExpression = $"UPDATE Doctors SET FirstName = '{entity.LastName}', Patronymic = '{entity.FirstName}', LastName ='{entity.Patronymic}', NumberPhone = '{entity.NumberPhone}' WHERE Id={entity.Id}";
                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    var getAllId = $"SELECT Id FROM Doctors WHERE Id ={entity.Id}";

                    var commandforGetAllId = new SqlCommand(getAllId, connection);
                    var readerId = commandforGetAllId.ExecuteReader();
                    var Id = new List<int>();
                    while (readerId.Read())
                    {
                        Id.Add(readerId.GetInt32(0));
                    }

                    if (Id.Any(el => el == entity.Id && entity.Id > 0))
                    {
                        var command = new SqlCommand(sqlExpression, connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
