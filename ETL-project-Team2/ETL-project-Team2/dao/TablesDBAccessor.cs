using ETL_project_Team2.models;
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
        private const string _dbConnectionString = "Data Source=localhost;Initial Catalog=TheOnlyUserDB;Integrated Security=True";
        private const string _tablesListRecordTable = "TablesList";

        [Obsolete]
        public void AddUserDataBase()
        {
            const string dbName = "master";
            string connectionString = string.Format(_dbConnectionString, dbName);
            using (var connection = new SqlConnection(connectionString))
            {
                const string command = "CREATE DATABASE @dataBaseName;";
                var sqlCommand = new SqlCommand(command, connection);
                sqlCommand.Parameters.AddWithValue("@dataBaseName", dbName);

                connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        [Obsolete]
        public void InitTableList()
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                const string command = "CREATE TABLE @tableName (@tableColumns);";
                var sqlCommand = new SqlCommand(command, connection);
                sqlCommand.Parameters.AddWithValue("@tableName", _tablesListRecordTable);
                sqlCommand.Parameters.AddWithValue("@tableColumns", "tableName NVARCHAR(MAX), tableColumns NVARCHAR(MAX)");

                connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void AddTableToRecords(SqlTable toBeAdded)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                const string commandString = "INSERT INTO @tablesListRecordName (tableName, tableColumns)\n" +
                    "VALUES (@tableName, @tableColumns);";
                var sqlCommand = new SqlCommand(commandString, connection);
                sqlCommand.Parameters.AddWithValue("@tablesListRecordName", _tablesListRecordTable);
                sqlCommand.Parameters.AddWithValue("@tableName", toBeAdded.TableName);
                sqlCommand.Parameters.AddWithValue("@tableColumns", JsonConvert.SerializeObject(toBeAdded.Coloumns));
                
                connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void CreateTable(ref SqlTable toBeCreated)
        {
            using(var connection = new SqlConnection(_dbConnectionString))
            {
                const string commandString = "CRAETE TABLE @tableName (@tableColumns);";
                string tableColumns = "";
                foreach(var columnPair in toBeCreated.Coloumns)
                    tableColumns += columnPair.Key + ' ' + columnPair.Value + " ,";
                tableColumns = tableColumns.TrimEnd(' ', ',');

                var sqlCommand = new SqlCommand(commandString, connection);
                sqlCommand.Parameters.AddWithValue("@tableName", toBeCreated.TableName);
                sqlCommand.Parameters.AddWithValue("@tableColumns", tableColumns);
                connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            toBeCreated.DBConnection = new SqlConnection(_dbConnectionString);
        }

        public SqlTable FindTable(string tableName)
        {
            SqlTable resultTable = new SqlTable()
            {
                TableName = tableName,
                DBConnection = new SqlConnection(_dbConnectionString)
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                const string commandString = "SELECT tableName FROM @tableName";
                var command = new SqlCommand(commandString);
                command.Parameters.AddWithValue("@tableName", tableName);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    resultTable.Coloumns = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                        reader["tableColumns"].ToString());
                }
            }
            return resultTable;
        }

        public List<string> FetchDataSetsList()
        {
            var result = new List<string>();
            using(var connection = new SqlConnection(_dbConnectionString))
            {
                const string commandString = "SELECT tableName FROM @tableName;";
                var sqlCommand = new SqlCommand(commandString, connection);
                sqlCommand.Parameters.AddWithValue("@tableName", _tablesListRecordTable);
                connection.Open();

                using(var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                        result.Add(reader["tableName"].ToString());
                }
            }
            return result;
        }
    }
}
