using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_project_Team2.dao
{
    public interface IPipelineDBAcessor
    {
        void AddPipelineModel(int modelId, string content, string userName);
        string FetchModel(int modelId, string userName);
        int UpdateModel(int modelId, string newContent, string userName);
    }
}
