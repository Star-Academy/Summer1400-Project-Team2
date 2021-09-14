using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ETL_project_Team2.models;

namespace ETL_project_Team2.dao
{
    public interface IDBAccessor
    {
        int ExecuteNonQuery(string cancellationToken, string queryCommand, SqlConnection dbConnection);
    }
}
