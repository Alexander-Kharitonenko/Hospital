//-----------------------------------------------------------------------
// <copyright file="DataBaseConfigurationManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>Pavel Hiruts</author>
// <summary>
// Implement capabilities of loading database from  file
// and dropping database.
// </summary>
//-----------------------------------------------------------------------

using Microsoft.SqlServer.Dac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace TicketManagement.IntegrationTests
{
    /// <summary>
    /// Implement capabilities of loading database from  file
    /// and dropping database.
    /// </summary>
    public class DataBaseConfigurationManager
    {
        /// <summary>
        /// store connection string.
        /// </summary>
        private string _connectionString;
        private string _dacPacString;

        /// <summary>
        /// store connection string.
        /// </summary>
        public string ConnectionString { get { return _connectionString; } }
        public string DacPacString { get { return _dacPacString; } }

        public DataBaseConfigurationManager()
        {
            string Path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "..//..//..//appsettings.json";

            using (StreamReader sr = new StreamReader(Path)) 
            {
                string json = sr.ReadToEnd();
                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    JsonElement root = document.RootElement;
                    var connectionStrings = root.GetProperty("ConnectionStrings").GetProperty("DefaultConnection").GetString();
                    var dacPacPath = root.GetProperty("AppSettings").GetProperty("dacpacFilePath").GetString();
                    _connectionString = connectionStrings;
                    _dacPacString = dacPacPath;
                    
                }

            }  
        }

        /// <summary>
        /// Loads database from file.
        /// </summary>
        public void LoadDataBase()
        {
            var dacOptions = new DacDeployOptions
            {
                CreateNewDatabase = true,
                IgnoreAuthorizer = true,
                IgnoreUserSettingsObjects = true,
            };

            var dacServiceInstance = new DacServices(_connectionString);

            //ConfigurationManager.AppSettings["dacpacFilePath"];
            var dacpacPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "..//..//..//..//..//" + DacPacString;



            if (File.Exists(dacpacPath))
            {
                using (DacPackage dacpac = DacPackage.Load(dacpacPath))
                {
                    dacServiceInstance.Deploy(dacpac, "HospitalTest", true, dacOptions);
                }
            }
            else
            {
                throw new Exception($"Error load database from dacpac file.({dacpacPath})");
            }
        }

        /// <summary>
        /// Drops database.
        /// </summary>
        public void DropDataBase()
        {
            _connectionString = ConnectionString;
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"
                        USE master;
                        alter database[HospitalTest] set single_user with rollback immediate;
                        drop database[HospitalTest]";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
