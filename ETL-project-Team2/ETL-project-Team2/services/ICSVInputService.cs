using System.Collections.Generic;
using System.Data.SqlClient;
using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    public interface ICSVInputService
    {
        Dictionary<string, string> GetColumnTypesAndNames(string filePath, char delim);
        void ImportDataToSql(SqlTable table, string filePath, char delim, bool hasHeader);
    }
}