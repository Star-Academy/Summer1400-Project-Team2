using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ETL_project_Team2.dao
{
    public class PipelineDBAccessor : IPipelineDBAcessor
    {
        private const string _dbConnectionString = "Data Source=localhost;Initial Catalog=modelsDB;Integrated Security=True";
        private const string _tableName = "PieplinesTable";

        public void AddPipelineModel(int modelId, string content)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = "INSERT INTO @tableName (@modelIdColumn, @contentColumn)\n" +
                    "VALUES (@modelId, @content);";
                var sqlCommand = new SqlCommand(commandString, connection);
                sqlCommand.Parameters.AddWithValue("@tableName", _tableName);
                sqlCommand.Parameters.AddWithValue("@modelIdColumn", "Id");
                sqlCommand.Parameters.AddWithValue("@contentColumn", "content");
                sqlCommand.Parameters.AddWithValue("@modelId", modelId);
                sqlCommand.Parameters.AddWithValue("@content", content);

                connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public string FetchModel(int modelId)
        {
            string content = null;
            using(var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = "SELECT @selectedColumn FROM @tableName;";
                var sqlCommand = new SqlCommand(commandString, connection);
                sqlCommand.Parameters.AddWithValue("@selectedColumn", "content");
                sqlCommand.Parameters.AddWithValue("@tableName", _tableName);

                connection.Open();
                using(var reader = sqlCommand.ExecuteReader())
                {
                    reader.Read();
                    content = reader["content"].ToString();
                }
            }
            return content;
        }
    }
}
