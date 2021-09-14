using System.Data.SqlClient;
using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    public interface ICSVInputService
    {
        void GetDataTableFromCsvFile(string filePath, string delimeter);
        void ImportDataToSql(SqlTable table);
    }
}