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
        //private const string _dbConnectionString = "Data Source=etldb,1433;Initial Catalog=TheOnlyUserDB;User Id=sa;Password=whaRHhiagaexLz9gkv3QQH05;";
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
            if (TableNameDuplicate(toBeAdded.TableName))
                throw new Exception($"A dataset named {toBeAdded.TableName} is already on database.");
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = $"INSERT INTO {_tablesListRecordTable} (tableName, tableColumns)\n" +
                    $"VALUES ('{toBeAdded.TableName}', '{JsonConvert.SerializeObject(toBeAdded.Coloumns)}');";
                var sqlCommand = new SqlCommand(commandString, connection);
                
                connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void CreateTable(ref SqlTable toBeCreated)
        {
            if (TableNameDuplicate(toBeCreated.TableName))
                throw new Exception($"A dataset named {toBeCreated.TableName} is already on database.");
            using(var connection = new SqlConnection(_dbConnectionString))
            {
                string tableColumns = "";
                foreach(var columnPair in toBeCreated.Coloumns)
                    tableColumns += columnPair.Key + ' ' + columnPair.Value + " ,";
                tableColumns = tableColumns.TrimEnd(' ', ',');

                string commandString = $"CREATE TABLE {toBeCreated.TableName} ({tableColumns});";
                var sqlCommand = new SqlCommand(commandString, connection);

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
                string commandString = $"SELECT tableColumns FROM {_tablesListRecordTable} WHERE tableName='{tableName}';";
                var command = new SqlCommand(commandString, connection);
                connection.Open();

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
                string commandString = $"SELECT tableName FROM {_tablesListRecordTable};";
                var sqlCommand = new SqlCommand(commandString, connection);
                connection.Open();

                using(var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                        result.Add(reader["tableName"].ToString());
                }
            }
            return result;
        }

        private bool TableNameDuplicate(string tableName)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = $"SELECT COUNT(*) FROM {_tablesListRecordTable} WHERE tableName='{tableName}';";
                using (var sqlCommand = new SqlCommand(commandString, connection))
                {
                    connection.Open();
                    return (int)sqlCommand.ExecuteScalar() > 0;
                }
            }
        }
    }
}
