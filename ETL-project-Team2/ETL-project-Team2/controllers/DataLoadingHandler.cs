using ETL_project_Team2.dao;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ETL_project_Team2.controllers
{
    public class DataLoadingHandler : IDataLoadingHandler
    {
        private IPipelineDBAcessor pipelineDB;
        private ITablesDBAccessor tablesDB;

        public DataLoadingHandler(IPipelineDBAcessor pipelineDBAccessor, ITablesDBAccessor tablesDBAccessor)
        {
            pipelineDB = pipelineDBAccessor;
            tablesDB = tablesDBAccessor;
        }

        [HttpGet]
        [Route("pipeline")]
        public IActionResult LoadListOfPipelines()
        {
            var modelNamesList = pipelineDB.FetchModelsList();
            var processedList = modelNamesList.Select(x => new { name = x });
            return new OkObjectResult(JsonConvert.SerializeObject(processedList));
        }

        [HttpGet]
        [Route("dataset")]
        public IActionResult LoadListtOfDataSets()
        {
            var dataSetNames = tablesDB.FetchDataSetsList();
            var processedList = dataSetNames.Select(x => new { name = x });
            return new OkObjectResult(JsonConvert.SerializeObject(processedList));
        }
    }
}
