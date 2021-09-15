namespace ETL_project_Team2.controllers
{
    public interface IAggregationHandler : IOperation
    {
        void Operate(string query);
    }
}