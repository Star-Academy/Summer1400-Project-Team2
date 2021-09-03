using ETL_project_Team2.services;

namespace ETL_project_Team2.models
{
    public class Node
    {
        public double XAxis { get; set; }
        public double YAxis { get; set; }
        public IOperation NodeOperation { get; set; }
        public SqlTable TempTable { get; set; }
        public Node NextNode { get; set; }
        public Node PreviousNode { get; set; }

        public void UpdateCoordinates(double x, double y)
        {
            XAxis = x;
            YAxis = y;
        }
    }
}