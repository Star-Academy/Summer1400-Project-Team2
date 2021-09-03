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
        int CreateTable(DataTable toBeCreated);
        int ExecuteNonQuery(DataTable dataTable, string queryCommand);
        int ExecuteNonQuery(DataTable dataTable, string queryStatement, params string[] selectClause);
        DataTable FecthSample(DataTable dataTable, int sampleSize, int fetchLevel);
    }
}
