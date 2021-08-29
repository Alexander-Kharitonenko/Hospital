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

using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Dac;
using System;
using System.Data.SqlClient;
using System.IO;

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

        /// <summary>
        /// store dacPac string.
        /// </summary>
        private string _dacPacString;

        /// <summary>
        /// property connection string.
        /// </summary>
        public string ConnectionString { get { return _connectionString; } }

        /// <summary>
        /// property DacPac string.
        /// </summary>
        public string DacPacString { get { return _dacPacString; } }

        /// <summary>
        /// property for Configuration 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// constructor DataBaseConfigurationManager
        /// </summary>
        public DataBaseConfigurationManager()
        {
            Configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "..//..//..//").AddJsonFile("appsettings.json").Build();
            var connectionStrings = Configuration["ConnectionStrings:DefaultConnection"];
            var dacPacPath = Configuration["AppSettings:dacpacFilePath"];
            _connectionString = connectionStrings;
            _dacPacString = dacPacPath;
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
            var dacpacPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + DacPacString;

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
