using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    public class NodeService
    {
        private static NodeService _nodeServiceInstance;

        private NodeService(){}
        public static NodeService GetInstance()
        {
            return _nodeServiceInstance ??= new NodeService();
        }

        public void Operate(Node node)
        {
            node.NodeOperation.Operate();
        }
    }
}