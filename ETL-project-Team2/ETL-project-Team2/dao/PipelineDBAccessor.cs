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

        public void AddPipelineModel(int modelId, string content, string userName)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = "INSERT INTO @tableName (@userName, @modelIdColumn, @contentColumn)\n" +
                    "VALUES (@modelId, @content);";
                var sqlCommand = new SqlCommand(commandString, connection);
                sqlCommand.Parameters.AddWithValue("@tableName", _tableName);
                sqlCommand.Parameters.AddWithValue("@userName", userName);
                sqlCommand.Parameters.AddWithValue("@modelIdColumn", "Id");
                sqlCommand.Parameters.AddWithValue("@contentColumn", "content");
                sqlCommand.Parameters.AddWithValue("@modelId", modelId);
                sqlCommand.Parameters.AddWithValue("@content", content);

                connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public string FetchModel(int modelId, string userName)
        {
            string content = null;
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = "SELECT @selectedColumn FROM @tableName WHERE @idColumn='@modelId' AND @userNameColumn='@userName';";
                var sqlCommand = new SqlCommand(commandString, connection);
                sqlCommand.Parameters.AddWithValue("@selectedColumn", "content");
                sqlCommand.Parameters.AddWithValue("@tableName", _tableName);
                sqlCommand.Parameters.AddWithValue("@idColumn", "Id");
                sqlCommand.Parameters.AddWithValue("@userNameColumn", "userName");
                sqlCommand.Parameters.AddWithValue("@userName", userName);

                connection.Open();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    reader.Read();
                    content = reader["content"].ToString();
                }
            }
            return content;
        }

        public int UpdateModel(int modelId, string newContent, string userName)
        {
            using(var connection = new SqlConnection(_dbConnectionString))
            {
                const string commandString = "UPDATE @tableName\n" +
                    "SET @contentColumn='@newContent'\n" +
                    "WHERE @modelIdColumn='@modelId' AND @userNameColumn='@userName'";
                var sqlCommand = new SqlCommand(commandString, connection);
                sqlCommand.Parameters.AddWithValue("@tableName", _tableName);
                sqlCommand.Parameters.AddWithValue("@contentColumn", "content");
                sqlCommand.Parameters.AddWithValue("@newContent", newContent);
                sqlCommand.Parameters.AddWithValue("@modelIdColumn", "Id");
                sqlCommand.Parameters.AddWithValue("@modelId", modelId);
                sqlCommand.Parameters.AddWithValue("@userNameColumn", "userName");
                sqlCommand.Parameters.AddWithValue("@userName", userName);

                connection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
