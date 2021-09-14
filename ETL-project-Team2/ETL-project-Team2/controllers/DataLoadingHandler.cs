using ETL_project_Team2.dao;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using ETL_project_Team2.services;
using ETL_project_Team2.models;

namespace ETL_project_Team2.controllers
{
    public class DataLoadingHandler : Controller, IDataLoadingHandler
    {
        [HttpGet]
        [Route("pipeline")]
        public IActionResult LoadListOfPipelines(IPipelineDBAcessor pipelineDB)
        {
            var modelNamesList = pipelineDB.FetchModelsList();
            var processedList = modelNamesList.Select(x => new { name = x });
            return new OkObjectResult(JsonConvert.SerializeObject(processedList));
        }

        [HttpGet]
        [Route("dataset")]
        public IActionResult LoadListOfDataSets(ITablesDBAccessor tablesDB)
        {
            var dataSetNames = tablesDB.FetchDataSetsList();
            var processedList = dataSetNames.Select(x => new { name = x });
            return new OkObjectResult(JsonConvert.SerializeObject(processedList));
        }

        [DisableRequestSizeLimit]
        public IActionResult PutCSVFileOnDB(ICSVInputService csvInputService, ITablesDBAccessor tablesDB)
        {
            HttpContext.Request.EnableBuffering();
            string filePath = $"{Path.GetTempPath()}//{Path.GetTempFileName()}.csv";
            using (var fileStream = System.IO.File.Create(filePath))
            {
                HttpContext.Request.Body.CopyTo(fileStream);
            }

            var sqlTable = new SqlTable()
            {
                TableName = $"userCSVFile#{DateTime.Now.ToString()}",
                Coloumns = new Dictionary<string, string>(csvInputService.GetDataTableFromCsvFile(filePath, ","))
            };

            tablesDB.AddTableToRecords(sqlTable);
            tablesDB.CreateTable(ref sqlTable);

            csvInputService.ImportDataToSql(sqlTable);
            return new OkResult();
        }
    }
}
