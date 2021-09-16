using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETL_project_Team2.models;
using ETL_project_Team2.services;
using ETL_project_Team2.dao;
using Newtonsoft.Json.Linq;
using System.IO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ETL_project_Team2.controllers
{
    public class PipelineHandler : Controller, IPipelineHandler
    {
        private LinkedList<Tuple<Node, IOperation>> _pipeline;
        private SqlTable _entryTable;
        private SqlTable _finalTable;
        private IPipelineDBAcessor pipelineDB;
        private ITablesDBAccessor tablesDB;
        private IDBService dbService;
        private IDBAccessor dbAccessor;

        public PipelineHandler(IPipelineDBAcessor pipelineDBAcessor, ITablesDBAccessor tablesDBAccessor,
            IDBService dbService, IDBAccessor dbAccessor)
        {
            pipelineDB = pipelineDBAcessor;
            tablesDB = tablesDBAccessor;
            this.dbService = dbService;
            this.dbAccessor = dbAccessor;
        }


        [HttpPost]
        [Route("pipeline")]
        public IActionResult CreateNewPipeline([FromBody] JObject content)
        {
            int modelId = pipelineDB.GetModelsCount();
            pipelineDB.AddPipelineModel(modelId, content["name"].ToString(), string.Empty,
                content["entryDB"].ToString(), content["finalDB"].ToString());
            return new OkObjectResult(modelId);
        }

        [HttpGet]
        [Route("pipeline/name/{modelId}")]
        public IActionResult GetPipelineName(int modelId)
        {
            string result = JsonConvert.SerializeObject(new { modelName = pipelineDB.FetchModelName(modelId) });
            return new OkObjectResult(result);
        }

        [HttpPut]
        [Route("pipeline/editName/{modelId}")]
        public IActionResult EditPipelineName(int modelId, [FromBody] JObject content)
        {
            pipelineDB.UpdateModelName(modelId, content["newName"].ToString());
            return new OkResult();
        }

        [HttpGet]
        [Route("pipeline/{modelId}")]
        public IActionResult GetPipeline(int modelId)
        {
            return new OkObjectResult(pipelineDB.FetchModel(modelId));
        }

        public IActionResult GetPreviewTable(int nodeId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("pipeline/operate")]
        public IActionResult OperatePipeline([FromQuery] int modelId)
        {
            LoadPipeline(modelId);
            SqlTable currentTable = _entryTable;
            foreach (var nodePair in _pipeline)
            {
                SqlTable nextTable = nodePair.Item2.Operate(currentTable);
                dbAccessor.ExecuteNonQuery("", dbService.DropTableQuery(currentTable), currentTable.DBConnection);
                currentTable = nextTable;
            }
            dbAccessor.ExecuteNonQuery("", dbService.CopyTableQuery(currentTable, _finalTable), _finalTable.DBConnection);
            dbAccessor.ExecuteNonQuery("", dbService.DropTableQuery(currentTable), currentTable.DBConnection);
            return new OkResult();
        }

        [HttpPut]
        [Route("pipeline/{modelId}")]
        public IActionResult UpdatePipeline(int modelId, [FromBody] JObject content)
        {
            pipelineDB.UpdateModel(modelId, content.ToString());
            return new OkResult();
        }

        [HttpPut]
        [Route("setParams/join/{modelId}")]
        [Route("setParams/filter/{modelId}")]
        [Route("setParams/aggregation/{modelId}")]
        public IActionResult SetNodeParams(int modelId, [FromBody] JObject content)
        {
            int nodeId = Int32.Parse(content["NodeId"].ToString());
            string parameters = content["Parameters"].ToString();

            LoadPipeline(modelId);
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
        private void LoadPipeline(int modelId)
        {
            var dbSets = pipelineDB.FetchPipelineDBs(modelId);
            _entryTable = tablesDB.FindTable(dbSets.Item1);
            _finalTable = tablesDB.FindTable(dbSets.Item2);

            string content = pipelineDB.FetchModel(modelId);
            var parsedObj = JObject.Parse(content);
            var jsonNodesList = (JArray)parsedObj["nodes"];
            var idsList = jsonNodesList.Select(x => x).
                Where(x =>
                {
                    string type = x["data"]["type"].ToString();
                    return type != "plus" && type != "destination" && type != "source";
                }).Select(x => x["id"]).ToList();
            List<Node> nodesList = idsList.Select(id => new Node()
            {
                Id = Int32.Parse(id.ToString())
            }).ToList();

            var rawOperationsList = parsedObj["nodes"].Select(x => x["data"]).Select(x => x["type"].ToString()).ToList();
            rawOperationsList.RemoveAll(x => { return x == "plus" || x == "source" || x == "destination"; });
            List<IOperation> operationsList = rawOperationsList.Select(operation => MakeOperation(operation)).ToList();

            _pipeline = new LinkedList<Tuple<Node, IOperation>>();
            for (int i = 0; i < operationsList.Count(); i++)
                _pipeline.AddLast(new Tuple<Node, IOperation>(nodesList[i], operationsList[i]));

            foreach (var nodePair in _pipeline)
            {
                string currentNodeParams = pipelineDB.FetchNodeParameters(modelId, nodePair.Item1.Id);
                if (!string.IsNullOrEmpty(currentNodeParams))
                    nodePair.Item2.SetParameters(currentNodeParams);
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
