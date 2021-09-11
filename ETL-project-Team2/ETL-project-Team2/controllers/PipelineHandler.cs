using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETL_project_Team2.models;
using ETL_project_Team2.services;
using ETL_project_Team2.dao;
using Newtonsoft.Json.Linq;

namespace ETL_project_Team2.controllers
{
    public class PipelineHandler : IPipelineHandler
    {
        private LinkedList<Tuple<Node, IOperation>> _pipeline;
        private string _currentModel;
        private SqlTable _entryTable;
        private SqlTable _finalTable;

        public IActionResult GetPipeline(int modelId, string userName, IPipelineDBAcessor pipeLineDB)
        {
            return new OkObjectResult(pipeLineDB.FetchModel(modelId, userName));
        }

        public IActionResult GetPreviewTable(int nodeId)
        {
            throw new NotImplementedException();
        }

        public IActionResult OpertePipeline(int modelId, string userName, IPipelineDBAcessor pipelineDB)
        {
            SqlTable currentTable = _entryTable;
            foreach (var nodePair in _pipeline)
            {
                currentTable = nodePair.Item2.Operate(currentTable, userName);
            }
            _finalTable = currentTable;
            return new OkResult();
        }

        public IActionResult UpdatePipeline(int modelId, string newContent, string userName, IPipelineDBAcessor pipelineDB)
        {
            pipelineDB.UpdateModel(modelId, newContent, userName);
            return new OkResult();
        }

        private void LoadPipeline(int modelId, string userName, IPipelineDBAcessor pipelineDB)
        {
            string content = pipelineDB.FetchModel(modelId, userName);
            var parsedObj = JObject.Parse(content);
            var idsList = (JArray)parsedObj["nodes"]["id"];
            List<Node> nodesList = idsList.Select(id => new Node()
            {
                Id = Int32.Parse(id.ToString())
            }).ToList();

            _pipeline = new LinkedList<Tuple<Node, IOperation>>
                (nodesList.Select(node => new Tuple<Node, IOperation>(node, null)));

            var rawJsonOperationsList = (JArray)parsedObj["nodes"]["data"]["type"];
            List<string> rawOperationsList = rawJsonOperationsList.Select(op => op.ToString()).ToList();
            rawOperationsList.RemoveAll(x => { return x == "plus" || x == "source" || x == "target"; });
            List<IOperation> operationsList = rawOperationsList.Select(operation => MakeOperation(operation)).ToList();

            _pipeline.Zip(operationsList, (tuple, operation) => new Tuple<Node, IOperation>(tuple.Item1, operation));
        }

        private IOperation MakeOperation(string type)
        {
            string processedInput = type.ToLower();
            switch(type)
            {
                case ("join"):
                    return new JoinHandler(new JoinService(), new DBAccessor(), new TablesDBAccessor());
                case ("aggregate"):
                    return null;
                case ("filter"):
                    return new FilterHandler(new DBAccessor(), new FilterService());
            }
            throw new ArgumentException(type + "is not a operation type");
        }
    }
}
