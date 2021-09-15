using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    public interface IAggregateService
    {
        string CreateQuery(SqlTable previousTable, AggregationModel aggregationModel);
        AggregationModel CreateModel(string jsonString);
    }
}