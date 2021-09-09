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

        private static SqlConnection dbConnection = new SqlConnection("");

        public int ExecuteNonQuery(string queryCommand)
        {
            using (var sqlCommand = new SqlCommand(queryCommand, dbConnection))
            {
                dbConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
