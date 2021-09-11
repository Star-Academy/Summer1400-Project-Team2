using ETL_project_Team2.models;

namespace ETL_project_Team2.controllers
{
    public interface IOperation
    {
        public SqlTable Operate(SqlTable table, string userName);
        public void SetParameters(string jsonString, string userName);
    }
}