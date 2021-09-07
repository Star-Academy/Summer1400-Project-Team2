using System.Data.SqlClient;
using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    public interface IFilterService
    {
        FilterModel GetFilterModel(string json);
    }
}