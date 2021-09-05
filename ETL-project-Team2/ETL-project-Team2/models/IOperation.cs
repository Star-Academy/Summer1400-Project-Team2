namespace ETL_project_Team2.models
{
    public interface IOperation
    {
        public SqlTable Operate(SqlTable table);
    }
}