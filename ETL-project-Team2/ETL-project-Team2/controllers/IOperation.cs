using ETL_project_Team2.models;

namespace ETL_project_Team2.controllers
{
    public interface IOperation
    {
        public SqlTable Operate(SqlTable table);
    }
}