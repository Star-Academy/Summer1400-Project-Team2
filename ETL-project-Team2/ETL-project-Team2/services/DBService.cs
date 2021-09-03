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
        public int CreateTable(DataTable toBeCreated)
        {
            const string commandTemplate = "CREATE TABLE {0} ({1});";
            string coloumns = string.Join(',', toBeCreated.Coloumns);
            using (var sqlCommand = new SqlCommand(string.Format(commandTemplate, toBeCreated.TableName, coloumns),
                toBeCreated.DBConnection))
                return sqlCommand.ExecuteNonQuery();
        }

        public int ExecuteNonQuery(DataTable dataTable, string queryCommand)
        {
            using (var sqlCommand = new SqlCommand(queryCommand, dataTable.DBConnection))
                return sqlCommand.ExecuteNonQuery();
        }

        public int ExecuteNonQuery(DataTable dataTable, string queryStatement, params string[] selectedColumns)
        {
            const string commandTemplate = "SELECT {0} FROM {1} {2}";
            string selectClause = string.Join(',', selectedColumns);
            using (var sqlCommand = new SqlCommand(string.Format(commandTemplate, selectClause, dataTable.TableName, queryStatement),
                dataTable.DBConnection))
                return sqlCommand.ExecuteNonQuery();

        }

        public DataTable FecthSample(DataTable dataTable, int sampleSize, int fetchLevel)
        {
            throw new NotImplementedException();
        }
    }
}
