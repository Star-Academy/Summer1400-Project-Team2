using ETL_project_Team2.controllers;
using ETL_project_Team2.models;

namespace ETL_project_Team2.dao
{
    public class NodeService
    {
        public IOperation Operator { get; set; }
        private Node _node; 

        public SqlTable Operate(SqlTable table)
        {
            return Operator.Operate(table);
        }

        public void LoadNode(Node node)
        {
            _node = node;
        }

        public void UpdateCoordinates(double x, double y)
        {
            _node.UpdateCoordinates(x, y);
        }
    }
}