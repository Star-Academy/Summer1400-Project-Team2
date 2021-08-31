using System.Data.SqlClient;

namespace ETL_project_Team2.services
{
    public interface IFilterService
    {
        void Filter(string query, string currentDBName, string destinationDBName, SqlConnection sqlConnection);
    }
}