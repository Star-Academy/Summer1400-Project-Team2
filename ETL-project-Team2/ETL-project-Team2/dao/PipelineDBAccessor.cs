using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ETL_project_Team2.dao
{
    public class PipelineDBAccessor : IPipelineDBAcessor
    {
        //private const string _dbConnectionString = "Data Source=localhost;Initial Catalog=ModelsDB;Integrated Security=True";
        private const string _dbConnectionString = "Data Source=etldb,1433;Initial Catalog=ModelsDB;User Id=sa;Password=whaRHhiagaexLz9gkv3QQH05;";
        private const string _pipelinesTableName = "PipelinesTable";

        private const string _nodesTableName = "NodesTable";

        public int GetModelsCount()
        {
            int count = 0;
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = $"SELECT COUNT(*) FROM  {_pipelinesTableName};";
                var sqlCommand = new SqlCommand(commandString, connection);

                connection.Open();
                count = (int)sqlCommand.ExecuteScalar();
            }
            return count;
        }

        public void AddPipelineModel(int modelId, string modelName, string content, string entryDB, string finalDB)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = $"INSERT INTO {_pipelinesTableName} (Id, name, content, entryDB, finalDB)\n" +
                    $"VALUES ('{modelId}', '{modelName}', '{content}', '{entryDB}', '{finalDB}');";
                var sqlCommand = new SqlCommand(commandString, connection);

                connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public string FetchModel(int modelId)
        {
            string content = null;
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = $"SELECT content FROM {_pipelinesTableName} WHERE Id='{modelId}';";
                var sqlCommand = new SqlCommand(commandString, connection);

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
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = $"UPDATE {_pipelinesTableName}\n" +
                    $"SET content='{newContent}'\n" +
                    $"WHERE Id='{modelId}';";
                var sqlCommand = new SqlCommand(commandString, connection);

                connection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }

        public int UpdateModelName(int modelId, string newName)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = $"UPDATE {_pipelinesTableName}\n" +
                    $"SET name='{newName}'\n" +
                    $"WHERE Id='{modelId}';";
                var sqlCommand = new SqlCommand(commandString, connection);

                connection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }

        public void SaveParameters(int modelId, int nodeId, string parameters)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string trueStatement = string.Format("UPDATE NodesTable SET params='{0}' WHERE nodeId='{1}' AND modelId='{2}';",
                        parameters, nodeId, modelId);
                string falseStatement = string.Format("INSERT INTO NodesTable (nodeId, modelId, params) VALUES ('{0}', '{1}', '{2}');",
                    nodeId, modelId, parameters);

                string commandString = "IF EXISTS (\n" +
                    "SELECT * FROM NodesTable\n" +
                    $"WHERE nodeId='{nodeId}' AND modelId='{modelId}')\n" +
                    "BEGIN\n" +
                    $"{trueStatement}\n" +
                    "END\n" +
                    "ELSE\n" +
                    "BEGIN\n" +
                    $"{falseStatement}\n" +
                    "END";
                
                var sqlCommand = new SqlCommand(commandString, connection);

                connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public string FetchNodeParameters(int modelId, int nodeId)
        {
            string result = "";
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = $"SELECT params FROM {_nodesTableName} WHERE modelId='{modelId}' AND nodeId='{nodeId}';";
                var sqlCommand = new SqlCommand(commandString, connection);

                connection.Open();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                        result = reader["params"].ToString();
                }
            }
            return result;
        }

        public Tuple<string, string> FetchPipelineDBs(int modelId)
        {
            Tuple<string, string> result = null;
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = $"SELECT entryDB, finalDB FROM {_pipelinesTableName} WHERE Id='{modelId}';";
                var sqlCommand = new SqlCommand(commandString, connection);

                connection.Open();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                        result = new Tuple<string, string>
                        (
                            reader["entryDB"].ToString(),
                            reader["finalDB"].ToString()
                        );
                }
            }
            return result;
        }

        public List<Tuple<string, string>> FetchModelsList()
        {
            var resultList = new List<Tuple<string, string>>();
            using(var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = $"SELECT name, Id FROM {_pipelinesTableName};";
                var sqlCommand = new SqlCommand(commandString, connection);
                connection.Open();

                using(var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                        resultList.Add(new Tuple<string,string>
                            (reader["name"].ToString(), reader["Id"].ToString()));
                }
            }
            return resultList;
        }

        public string FetchModelName(int modelId)
        {
            string result = null;
            using(var connection = new SqlConnection(_dbConnectionString))
            {
                string commandString = $"SELECT name FROM {_pipelinesTableName} WHERE Id='{modelId}';";
                var sqlCommand = new SqlCommand(commandString, connection);
                connection.Open();

                using(var reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                        result = reader["name"].ToString();
                }
            }
            return result;
        }
    }
}
