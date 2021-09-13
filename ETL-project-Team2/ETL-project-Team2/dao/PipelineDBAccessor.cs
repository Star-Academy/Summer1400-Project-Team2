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
        private const string _pipelinesTableName = "PipelinesTable";

        private const string _nodesTableName = "NodesTable";

        public void AddPipelineModel(int modelId, string content)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = "INSERT INTO @tableName (@modelIdColumn, @contentColumn)\n" +
                    "VALUES (@modelId, @content);";
                var sqlCommand = new SqlCommand(commandString, connection);
                sqlCommand.Parameters.AddWithValue("@tableName", _pipelinesTableName);
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
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = "SELECT @selectedColumn FROM @tableName WHERE @idColumn='@modelId';";
                var sqlCommand = new SqlCommand(commandString, connection);
                sqlCommand.Parameters.AddWithValue("@selectedColumn", "content");
                sqlCommand.Parameters.AddWithValue("@tableName", _pipelinesTableName);
                sqlCommand.Parameters.AddWithValue("@idColumn", "Id");
                sqlCommand.Parameters.AddWithValue("@modelId", modelId);

                connection.Open();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    reader.Read();
                    content = reader["content"].ToString();
                }
            }
            return content;
        }

        public int UpdateModel(int modelId, string newContent)
        {
            using(var connection = new SqlConnection(_dbConnectionString))
            {
                const string commandString = "UPDATE @tableName\n" +
                    "SET @contentColumn='@newContent'\n" +
                    "WHERE @modelIdColumn='@modelId';";
                var sqlCommand = new SqlCommand(commandString, connection);
                sqlCommand.Parameters.AddWithValue("@tableName", _pipelinesTableName);
                sqlCommand.Parameters.AddWithValue("@contentColumn", "content");
                sqlCommand.Parameters.AddWithValue("@newContent", newContent);
                sqlCommand.Parameters.AddWithValue("@modelIdColumn", "Id");
                sqlCommand.Parameters.AddWithValue("@modelId", modelId);

                connection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }

        public void SaveParameters(int modelId, int nodeId, string parameters)
        {
            using(var connection = new SqlConnection(_dbConnectionString))
            {
                const string commandString = "IF EXISTS (\n" +
                    "SELECT * FROM NodesTable\n" +
                    "WHERE nodeId='@nodeId' AND modelId='@modelId')\n" +
                    "BEGIN\n" +
                    "@trueStatement\n" +
                    "END\n" +
                    "ELSE\n" +
                    "BEGIN\n" +
                    "@falseStatement\n" +
                    "END";
                string trueStatement = string.Format("UPDATE NodesTable SET params='{0}' WHERE nodeId='{1}' AND modelId='{2}';", 
                    parameters, nodeId, modelId);
                string falseStatement = string.Format("INSERT INTO NodesTable (nodeId, modelId, params) VALUES ({0}, {1}, {2});", 
                    nodeId, modelId, parameters);
                var sqlCommand = new SqlCommand(commandString, connection);
                sqlCommand.Parameters.AddWithValue("@nodeId", nodeId);
                sqlCommand.Parameters.AddWithValue("@modelId", modelId);
                sqlCommand.Parameters.AddWithValue("@trueStatement", trueStatement);
                sqlCommand.Parameters.AddWithValue("@falseStatement", falseStatement);

                connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public string FetchNodeParameters(int modelId, int nodeId)
        {
            string result = "";
            using(var connection = new SqlConnection(_dbConnectionString))
            {
                const string commandString = "SELECT params FROM @tableName WHERE modelId='@modelId' AND nodeId='@nodeId';";
                var sqlCommand = new SqlCommand(commandString, connection);
                sqlCommand.Parameters.AddWithValue("@tableName", _nodesTableName);
                sqlCommand.Parameters.AddWithValue("@modelId", modelId);
                sqlCommand.Parameters.AddWithValue("@nodeId", nodeId);

                connection.Open();
                using(var reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                        result = reader["params"].ToString();
                }
            }
            return result;
        }
    }
}
