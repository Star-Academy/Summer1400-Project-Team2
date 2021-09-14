using System.Collections.Generic;
using System.Data.SqlClient;
using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    public interface ICSVInputService
    {
        Dictionary<string, string> GetDataTableFromCsvFile(string filePath, string delimeter);
     //   Dictionary<string, string> GetCSVColumns(string filePath, string delimeter);
        void ImportDataToSql(SqlTable table);
    }
}