﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_project_Team2.dao
{
    public interface IPipelineDBAcessor
    {
        int GetModelsCount();
        void AddPipelineModel(int modelId, string modelName, string content, string entryDB, string finalDB);
        string FetchModel(int modelId);
        int UpdateModel(int modelId, string newContent);
        int UpdateModelName(int modelId, string newName);
        void SaveParameters(int modelId, int nodeId, string parameters);
        string FetchNodeParameters(int modelId, int nodeId);
        public Tuple<string, string> FetchPipelineDBs(int modelId);
        public List<Tuple<string, string>> FetchModelsList();
    }
}
