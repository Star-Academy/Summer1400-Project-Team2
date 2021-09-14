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
    public class PipelineHandler : Controller, IPipelineHandler
    {
        private LinkedList<Tuple<Node, IOperation>> _pipeline;
        private SqlTable _entryTable;
        private SqlTable _finalTable;

        [HttpPost]
        [Route("pipeline")]
        public IActionResult CreateNewPipeline([FromBody] JObject content, IPipelineDBAcessor pipelineDB)
        {
            int modelId = pipelineDB.GetModelsCount();
            pipelineDB.AddPipelineModel(modelId, content["name"].ToString(), "",
                content["entryDB"].ToString(), content["finalDB"].ToString());
            return new OkObjectResult(modelId);
        }

        [HttpPut]
        [Route("pipeline/editName")]
        public IActionResult EditPipelineName([FromQuery] int modelId, [FromQuery] string name, IPipelineDBAcessor pipelineDB)
        {
            pipelineDB.UpdateModelName(modelId, name);
            return new OkResult();
        }

        [HttpGet]
        [Route("pipeline/{modelId}")]
        public IActionResult GetPipeline(int modelId, IPipelineDBAcessor pipeLineDB)
        {
            return new OkObjectResult(pipeLineDB.FetchModel(modelId));
        }

        public IActionResult GetPreviewTable(int nodeId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("pipeline/operate")]
        public IActionResult OperatePipeline([FromQuery] int modelId,
            IPipelineDBAcessor pipelineDB, ITablesDBAccessor tablesDB)
        {
            LoadPipeline(modelId, pipelineDB, tablesDB);
            SqlTable currentTable = _entryTable;
            foreach (var nodePair in _pipeline)
            {
                currentTable = nodePair.Item2.Operate(currentTable);
            }
            _finalTable = currentTable;
            return new OkResult();
        }

        public IActionResult UpdatePipeline([FromQuery] int modelId, [FromBody] JObject content,
            IPipelineDBAcessor pipelineDB, ITablesDBAccessor tablesDB)
        {
            pipelineDB.UpdateModel(modelId, content["content"].ToString());
            LoadPipeline(modelId, pipelineDB, tablesDB);
            return new OkResult();
        }

        [HttpPut]
        [Route("setParams/join/{modelId}")]
        [Route("setParams/filter/{modelId}")]
        [Route("setParams/aggregation/{modelId}")]
        public IActionResult SetNodeParams(int modelId, [FromBody] JObject content,
            IPipelineDBAcessor pipelineDB, ITablesDBAccessor tablesDB)
        {
            int nodeId = Int32.Parse(content["NodeId"].ToString());
            string parameters = content["Parameters"].ToString();

            LoadPipeline(modelId, pipelineDB, tablesDB);
            foreach (var nodePair in _pipeline)
            {
                if (nodePair.Item1.Id == nodeId)
                {
                    nodePair.Item2.SetParameters(parameters);
                    pipelineDB.SaveParameters(modelId, nodeId, parameters);
                    return new OkResult();
                }
            }
            return new NotFoundResult();
        }

        //This method doesn't matter skip it
        private void LoadPipeline(int modelId, IPipelineDBAcessor pipelineDB, ITablesDBAccessor tablesDB)
        {
            var dbSets = pipelineDB.FetchPipelineDBs(modelId);
            _entryTable = tablesDB.FindTable(dbSets.Item1);
            _finalTable = tablesDB.FindTable(dbSets.Item2);

            string content = pipelineDB.FetchModel(modelId);
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

            foreach (var nodePair in _pipeline)
            {
                nodePair.Item2.SetParameters(pipelineDB.FetchNodeParameters(modelId, nodePair.Item1.Id));
            }
        }

        private IOperation MakeOperation(string type)
        {
            string processedInput = type.ToLower();
            switch (type)
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
