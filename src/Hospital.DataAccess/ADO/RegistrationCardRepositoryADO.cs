using Hospital.DataAccess.Entity;
using Hospital.DataAccess.Interfaces;
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
    public class RegistrationCardRepositoryADO : BaseRepositoryADO<RegistrationCard> , IRegistrationCardRepository
    {
        public RegistrationCardRepositoryADO(string connectionString) : base(connectionString)
        { }

        /// <summary>
        /// add a new object to the database
        /// </summary>
        /// <param name="entity">object to add</param>
        /// <returns>void</returns>
        public async override Task CreateEntity(RegistrationCard entity)
        {
            var data = entity.DateAdmission.ToShortDateString().Replace(".", "-").ToString();
            if (entity != null)
            {
                string sqlExpression = $"INSERT INTO RegistrationСards (DoctorId,PatientId,DiagnosisId,DateAdmission) VALUES ('{entity.DoctorId}', '{entity.PatientId}', '{entity.DiagnosisId}', {data})";
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
        public async override Task Delete(RegistrationCard entity)
        {
            List<RegistrationCard> result = new List<RegistrationCard>();
            var getEntityById = $"SELECT * FROM RegistrationСards WHERE Id ={entity.Id}";

            if (entity != null)
            {
                var sqlExpression = $"DELETE FROM RegistrationСards WHERE Id= {entity.Id}";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand commandforGetAllId = new SqlCommand(getEntityById, connection);
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
        public override IEnumerable<RegistrationCard> Get()
        {
            List<RegistrationCard> result = new List<RegistrationCard>();
            var sqlExpression = "SELECT * FROM RegistrationСards";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new RegistrationCard() { Id = reader.GetInt32(0), PatientId = reader.GetInt32(2), DoctorId = reader.GetInt32(1), DiagnosisId = reader.GetInt32(4), DateAdmission = reader.GetDateTime(3) });
                }
                return result;
            }
        }

        /// <summary>
        /// returns all elements that match a condition
        /// </summary>
        /// <param name="predicate">condition</param>
        /// <returns>IEnumerable<Doctor></returns>
        public override IEnumerable<RegistrationCard> GetAllEntityBy(Expression<Func<RegistrationCard, bool>> predicate)
        {
            List<RegistrationCard> result = new List<RegistrationCard>();
            int item;
            var argument = predicate.ToString().Replace("el => (el.Id == ", string.Empty).Replace(")", string.Empty); ;
            var x = int.TryParse(argument, out item);
            var predicateString = predicate.ToString().Replace("el => (el.", string.Empty).Replace(")", string.Empty).Replace("==", "=");
            var sqlExpression = $"SELECT * FROM RegistrationСards WHERE {predicateString}";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var GetAllId = "SELECT * FROM RegistrationСards Id";

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
                        result.Add(new RegistrationCard() { Id = reader.GetInt32(0), PatientId = reader.GetInt32(2), DoctorId = reader.GetInt32(1), DiagnosisId = reader.GetInt32(4), DateAdmission = reader.GetDateTime(3) });
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
        public async override Task Update(RegistrationCard entity)
        {
            if (entity != null)
            {
                //'{x.ToString()}' and DATE
                //{x} and DATETIME
                var x = entity.DateAdmission.ToShortDateString().Replace(".", "-");
                var sqlExpression = $"UPDATE RegistrationСards SET PatientId = '{entity.PatientId}',DoctorId = '{entity.DoctorId}',DiagnosisId = '{entity.DiagnosisId}',DateAdmission = {x} WHERE Id={entity.Id}";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    var GetAllId = "SELECT * FROM RegistrationСards Id";
                    await connection.OpenAsync();
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
