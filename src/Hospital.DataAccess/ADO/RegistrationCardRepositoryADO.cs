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
    public class RegistrationCardRepositoryADO : BaseRepositoryADO<RegistrationCard>
    {
        public RegistrationCardRepositoryADO(string connectionString) : base(connectionString)
        { }

        public async override Task CreateEntity(RegistrationCard entity)
        {
            var x = entity.DateAdmission.ToShortDateString().Replace(".", "-").ToString();
            if (entity != null)
            {
                string sqlExpression = $"INSERT INTO RegistrationСards (DoctorId,PatientId,DiagnosisId,DateAdmission) VALUES ('{entity.DoctorId}', '{entity.PatientId}', '{entity.DiagnosisId}', {x})";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                    
                }
            }
           

        }

        public async override Task Delete(RegistrationCard entity)
        {
            if (entity != null)
            {
                string sqlExpression = $"DELETE FROM RegistrationСards WHERE Id= {entity.Id}";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                    


                }
            }

        }

        public override IEnumerable<RegistrationCard> Get()
        {
            List<RegistrationCard> result = new List<RegistrationCard>();
            string sqlExpression = "SELECT * FROM RegistrationСards";
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

        public override IEnumerable<RegistrationCard> GetAllEntityBy(Expression<Func<RegistrationCard, bool>> predicate)
        {
            List<RegistrationCard> result = new List<RegistrationCard>();
            int item;
            string argument = predicate.ToString().Replace("el => (el.Id == ", string.Empty).Replace(")", string.Empty); ;
            var x = int.TryParse(argument, out item);
            string predicateString = predicate.ToString().Replace("el => (el.", string.Empty).Replace(")", string.Empty).Replace("==", "=");
            string sqlExpression = $"SELECT * FROM RegistrationСards WHERE {predicateString}";


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string GetAllId = "SELECT * FROM RegistrationСards Id";

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

        public async override Task Update(RegistrationCard entity)
        {
            if (entity != null)
            {
                //'{x.ToString()}' and DATE
                //{x} and DATETIME
                var x = entity.DateAdmission.ToShortDateString().Replace(".", "-");
                string sqlExpression = $"UPDATE RegistrationСards SET PatientId = '{entity.PatientId}',DoctorId = '{entity.DoctorId}',DiagnosisId = '{entity.DiagnosisId}',DateAdmission = {x} WHERE Id={entity.Id}";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    
                    string GetAllId = "SELECT * FROM RegistrationСards Id";
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
