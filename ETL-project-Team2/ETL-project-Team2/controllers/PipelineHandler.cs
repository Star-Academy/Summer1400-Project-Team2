using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETL_project_Team2.models;
using ETL_project_Team2.services;
using ETL_project_Team2.dao;

namespace ETL_project_Team2.controllers
{
    public class PipelineHandler : IPipelineHandler
    {
        private LinkedList<Tuple<Node, IOperation>> _pipeline;
        private string _currentModel;
        private SqlTable _entryTable;
        private SqlTable _finalTable;

        public IActionResult GetPipeline(int modelId, IPipelineDBAcessor pipeLineDB)
        {
            return new OkObjectResult(pipeLineDB.FetchModel(modelId));
        }

        public IActionResult GetPreviewTable(int nodeId)
        {
            throw new NotImplementedException();
        }

        public IActionResult OpertePipeline(int modelId)
        {
            throw new NotImplementedException();
        }

        public IActionResult UpdatePipeline(string jsonString)
        {
            throw new NotImplementedException();
        }

        private void LoadPipeline(int modelId)
        {
            
        }
    }
}
