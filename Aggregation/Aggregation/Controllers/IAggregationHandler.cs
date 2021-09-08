namespace Aggregation.Controllers
{
    public interface IAggregationHandler : IOperation
    {
        void Operate(string query);
    }
}