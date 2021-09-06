using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    public class PipelineHandler
    {
        public List<NodeService> Pipeline { get; set; }
        public string Name { get; set; }
        public SqlTable EntryTable { get; set; }
        public SqlTable FinalTable { get; set; }
        public string JsonParameters { get; set; }

        public void SetParameters(string jsonParameters)
        {
            // TODO make pipeline from parameters
        }
        
        public PipelineHandler(SqlTable entryTable, SqlTable finalTable, string jsonParameters)
        {
            EntryTable = entryTable;
            FinalTable = finalTable;
            JsonParameters = jsonParameters;
            SetParameters(jsonParameters);
        }
        
        public void OperatePipeline()
        {
            SqlTable curTable = EntryTable ?? throw new ArgumentNullException(nameof(EntryTable));
            foreach (NodeService nodeService in Pipeline)
            {
                curTable = nodeService.Operate(curTable);
            }
            FinalTable = curTable; // TODO check finalTable in sql should be changed.
        }
    }
    
}