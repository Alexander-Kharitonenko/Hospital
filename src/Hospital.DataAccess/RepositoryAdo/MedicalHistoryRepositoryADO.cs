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
    /// contains the logic for working with MedicalHistoryRepository using ADO technology
    /// </summary>
    public class MedicalHistoryRepositoryAdo : BaseRepositoryAdo<MedicalHistory>, IMedicalHistoryRepository
    {
        /// <summary>
        /// constructor for initializing class fields
        /// </summary>
        /// <param name="connectionString">database connection field</param>
        public MedicalHistoryRepositoryAdo(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// add a new object to the database
        /// </summary>
        /// <param name="entity">object to add</param>
        /// <returns>void</returns>
        public async override Task CreateEntity(MedicalHistory entity)
        {
            if (entity != null)
            {
                var sqlExpression = $"INSERT INTO MedicalHistorys (Diagnosis) VALUES ('{entity.Diagnosis}')";
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
        public async override Task Delete(MedicalHistory entity)
        {
            var result = new List<MedicalHistory>();
            var getEntityById = $"SELECT * FROM MedicalHistorys WHERE Id ={entity.Id}";

            if (entity != null)
            {
                var sqlExpression = $"DELETE FROM MedicalHistorys WHERE Id= {entity.Id}";
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
        public override IEnumerable<MedicalHistory> Get()
        {
            var result = new List<MedicalHistory>();
            var sqlExpression = "SELECT * FROM MedicalHistorys";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new MedicalHistory() { Id = reader.GetInt32(0), Diagnosis = reader.GetString(1) });
                }
                return result;
            }
        }

        /// <summary>
        /// Update a object to the database
        /// </summary>
        /// <param name="entity">object to Update</param>
        /// <returns>void</returns>
        public async override Task Update(MedicalHistory entity)
        {
            if (entity != null)
            {
                var sqlExpression = $"UPDATE MedicalHistorys SET Diagnosis = '{entity.Diagnosis}'";
                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    var getAllId = $"SELECT Id FROM MedicalHistorys WHERE Id = {entity.Id}";

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
