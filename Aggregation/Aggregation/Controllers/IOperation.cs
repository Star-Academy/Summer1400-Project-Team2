namespace Aggregation.Controllers
{
    public interface IOperation
    {
        public SqlTable Operate(SqlTable table);
        public void SetParameters(string jsonString);
    }
}