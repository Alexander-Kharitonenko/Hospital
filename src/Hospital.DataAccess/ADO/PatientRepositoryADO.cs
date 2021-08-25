using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;
using RepositoryADO.InterfaceForRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hospital.DataAccess.ADO
{
    /// <summary>
    /// contains the logic for working with PatientRepository using ADO technology
    /// </summary>
    public class PatientRepositoryAdo : BaseRepositoryAdo<Patient>, IPatientRepository
    {
        /// <summary>
        /// constructor for initializing class fields
        /// </summary>
        /// <param name="connectionString">database connection field</param>
        public PatientRepositoryAdo(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// add a new object to the database
        /// </summary>
        /// <param name="entity">object to add</param>
        /// <returns>void</returns>
        public async override Task CreateEntity(Patient entity)
        {
            if (entity != null)
            {
                var sqlExpression = $"INSERT INTO Patients (FirstName,Patronymic,LastName,Gender,ResidenceAddress) VALUES ('{entity.FirstName}','{entity.Patronymic}', '{entity.LastName}', '{entity.Gender}','{entity.ResidenceAddress}')";
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
        public async override Task Delete(Patient entity)
        {
            var result = new List<MedicalHistory>();
            var getEntityById = $"SELECT * FROM Patients WHERE Id ={entity.Id}";

            if (entity != null)
            {
                var sqlExpression = $"DELETE FROM Patients WHERE Id= {entity.Id}";
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
        public override IEnumerable<Patient> Get()
        {
            var result = new List<Patient>();
            var sqlExpression = "SELECT * FROM Patients";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Patient() { Id = reader.GetInt32(0), FirstName = reader.GetString(2), Patronymic = reader.GetString(3), LastName = reader.GetString(1), Gender = reader.GetString(4), ResidenceAddress = reader.GetString(5) });
                }
                return result;
            }
        }

        /// <summary>
        /// returns all elements that match a condition
        /// </summary>
        /// <param name="predicate">condition</param>
        /// <returns>IEnumerable<Doctor></returns>
        public override IEnumerable<Patient> GetAllEntityBy(Expression<Func<Patient, bool>> predicate)
        {
            var result = new List<Patient>();
            int item;
            var argument = predicate.ToString().Replace("el => (el.Id == ", string.Empty).Replace(")", string.Empty); ;
            var x = int.TryParse(argument, out item);
            var predicateString = predicate.ToString().Replace("el => (el.", string.Empty).Replace(")", string.Empty).Replace("==", "=");
            var sqlExpression = $"SELECT * FROM Patients WHERE {predicateString}";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var getAllId = "SELECT * FROM Patients Id";

                var commandforGetAllId = new SqlCommand(getAllId, connection);
                var readerId = commandforGetAllId.ExecuteReader();
                var Id = new List<int>();
                while (readerId.Read())
                {
                    Id.Add(readerId.GetInt32(0));
                }

                if (Id.Any(el => el == item && item > 0))
                {
                    var command = new SqlCommand(sqlExpression, connection);
                    var reader = command.ExecuteReader();
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

        /// <summary>
        /// Update a object to the database
        /// </summary>
        /// <param name="entity">object to Update</param>
        /// <returns>void</returns>
        public async override Task Update(Patient entity)
        {
            if (entity != null)
            {
                var sqlExpression = $"UPDATE Patients SET FirstName = '{entity.FirstName}',Patronymic = '{entity.Patronymic}',LastName = '{entity.LastName}',Gender = '{entity.Gender}',ResidenceAddress = '{entity.ResidenceAddress}' WHERE Id={entity.Id}";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    var getAllId = "SELECT * FROM Patients Id";
                    await connection.OpenAsync();
                    var commandforGetAllId = new SqlCommand(getAllId, connection);
                    var readerId = commandforGetAllId.ExecuteReader();
                    var Id = new List<int>();
                    while (readerId.Read())
                    {
                        Id.Add(readerId.GetInt32(0));
                    }

                    if (Id.Any(el => el > entity.Id && entity.Id > 0))
                    {
                        var command = new SqlCommand(sqlExpression, connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
