using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    public interface IDBService
    {
        int CreateTable(SqlTable toBeCreated);
        int ExecuteNonQuery(SqlConnection dbConnecton, string queryCommand);
        SqlTable FecthSample(SqlTable dataTable, int sampleSize, int fetchLevel);
    }
}
