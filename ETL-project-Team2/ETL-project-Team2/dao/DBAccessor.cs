using ETL_project_Team2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ETL_project_Team2.dao
{
    public class DBAccessor : IDBAccessor
    {
        public int ExecuteNonQuery(string queryCommand, SqlConnection dbConnection)
        {
            using (var sqlCommand = new SqlCommand(queryCommand, dbConnection))
            {
                dbConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
