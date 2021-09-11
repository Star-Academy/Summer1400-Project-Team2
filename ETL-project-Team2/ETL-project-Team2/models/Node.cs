using ETL_project_Team2.services;

namespace ETL_project_Team2.models
{
    public class Node
    {
        public int Id { get; set; }
        public double XAxis { get; set; }
        public double YAxis { get; set; }
        public SqlTable TempTable { get; set; }
    }
}