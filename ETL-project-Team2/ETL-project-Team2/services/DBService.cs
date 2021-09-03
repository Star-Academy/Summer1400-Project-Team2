using ETL_project_Team2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ETL_project_Team2.services
{
    public class DBService : IDBService
    {
        public int CreateTable(SqlTable toBeCreated)
        {
            const string commandTemplate = "CREATE TABLE {0} ({1});";
            string columns = ColoumnPairsToString(toBeCreated.Coloumns);
            using (var sqlCommand = new SqlCommand(string.Format(commandTemplate, toBeCreated.TableName, columns),
                toBeCreated.DBConnection))
                return sqlCommand.ExecuteNonQuery();
        }

        public int ExecuteNonQuery(SqlTable dataTable, string queryCommand)
        {
            using (var sqlCommand = new SqlCommand(queryCommand, dataTable.DBConnection))
                return sqlCommand.ExecuteNonQuery();
        }

        public SqlTable FecthSample(SqlTable dataTable, int sampleSize, int fetchLevel)
        {
            throw new NotImplementedException();
        }

        private string ColoumnPairsToString(Dictionary<string, string> columns)
        {
            string result = "";
            foreach (KeyValuePair<string, string> columnPair in columns)
                result += columnPair.Key + ' ' + columnPair.Value + ',';
            result.TrimEnd(',', ' ');
            return result;
        }
    }
}
