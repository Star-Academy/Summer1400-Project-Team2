﻿using ETL_project_Team2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace ETL_project_Team2.dao
{
    public class TablesDBAccessor : ITablesDBAccessor
    {
        private const string dbConnectionStringPattern = "Data Source=@ServerName;" +
            "Initial Catalog=@DataBaseName;" +
            "User id=@UserName;" +
            "Password=@Secret;";
        private const string dbNamePattern = "{0}DB";
        private const string tablesListRecordTable = "TablesList";

        public void AddTable(string userName, SqlTable toBeAdded)
        {
            string connectionString = string.Format(dbConnectionStringPattern,
                string.Format(dbNamePattern, userName));

            using (var connection = new SqlConnection(connectionString))
            {
                const string commandString = "INSERT INTO @tablesListRecordName (tableName, tableColumns)\n" +
                    "VALUES (@tableName, @tableColumns);";
                var sqlCommand = new SqlCommand(commandString, connection);
                sqlCommand.Parameters.AddWithValue("@tablesListRecordName", tablesListRecordTable);
                sqlCommand.Parameters.AddWithValue("@tableName", toBeAdded.TableName);
                sqlCommand.Parameters.AddWithValue("@tableColumns", JsonConvert.SerializeObject(toBeAdded.Coloumns));

                connection.Open();
                sqlCommand.ExecuteNonQuery();
            }

        }

        public SqlTable FindTable(string tableName, string userName)
        {
            string connectionString = string.Format(dbConnectionStringPattern,
                    string.Format(dbNamePattern, userName));
            SqlTable resultTable = new SqlTable()
            {
                TableName = tableName,
                DBConnection = new SqlConnection(connectionString)
            };

            using(var connection = new SqlConnection(connectionString))
            {
                const string commandString = "SELECT tableName FROM @tableName";
                var command = new SqlCommand(commandString);
                command.Parameters.AddWithValue("@tableName", tableName);

                using(var reader = command.ExecuteReader())
                {
                    reader.Read();
                    resultTable.Coloumns = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                        reader["tableColumns"].ToString());
                }
            }
            return resultTable;
        }
    }
}
