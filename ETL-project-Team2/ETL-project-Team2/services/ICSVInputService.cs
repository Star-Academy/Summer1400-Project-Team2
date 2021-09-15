using System.Collections.Generic;
using System.Data.SqlClient;
using ETL_project_Team2.models;
using System.IO;

namespace ETL_project_Team2.services
{
    public interface ICSVInputService
    {
        Dictionary<string, string> GetColumnTypesAndNames(Stream inputStream, char delim);
        void ImportDataToSql(SqlTable table, string filePath, char delim, bool hasHeader);
    }
}