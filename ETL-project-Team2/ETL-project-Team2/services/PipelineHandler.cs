using System.Collections.Generic;
using System.Data.SqlClient;
using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    public class PipelineHandler
    {
        public List<Node> Pipeline { get; set; }
        public string Name { get; set; }
        public SqlConnection EntryDbConnection { get; set; }
        public SqlConnection FinalDbConnection { get; set; }
        public string FilePath { get; set; }
        
        public void OperatePipeline(){
            foreach (Node node in Pipeline)
            {
                NodeService.GetInstance().Operate(node);
            }
        }
    }
    
}