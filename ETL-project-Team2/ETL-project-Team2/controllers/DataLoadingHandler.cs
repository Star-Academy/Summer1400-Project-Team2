﻿using ETL_project_Team2.dao;
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
using Newtonsoft.Json.Linq;

namespace ETL_project_Team2.controllers
{
    public class DataLoadingHandler : Controller, IDataLoadingHandler
    {
        private IPipelineDBAcessor pipelineDB;
        private ITablesDBAccessor tablesDB;
        private ICSVInputService csvInputService;
        private ICSVOutputHandler csvOutputHandler;
        private const string filesPath = "F:";

        public DataLoadingHandler(IPipelineDBAcessor pipelineDBAccessor, ITablesDBAccessor tablesDBAccessor,
            ICSVInputService csvInputService, ICSVOutputHandler csvOutputHandler)
        {
            pipelineDB = pipelineDBAccessor;
            tablesDB = tablesDBAccessor;
            this.csvInputService = csvInputService;
            this.csvOutputHandler = csvOutputHandler;
        }

        [HttpGet]
        [Route("pipeline")]
        public IActionResult LoadListOfPipelines()
        {
            var modelNamesList = pipelineDB.FetchModelsList();
            var processedList = modelNamesList.Select(x => new { name = x.Item1, id = x.Item2 });
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

        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("dataset")]
        public IActionResult PutCSVFileOnDB([FromQuery] string dataSetName)
        {
            var sqlTable = new SqlTable()
            {
                TableName = dataSetName,
                Coloumns = new Dictionary<string, string>(csvInputService.GetColumnTypesAndNames(HttpContext.Request.Body, ','))
            };

            string filePath = $"{filesPath}{Path.DirectorySeparatorChar}{dataSetName}tempFile.csv";
            using (var fileStream = System.IO.File.Create(filePath))
            {
                HttpContext.Request.Body.CopyTo(fileStream);
            }

            tablesDB.CreateTable(ref sqlTable);
            tablesDB.AddTableToRecords(sqlTable);

            csvInputService.ImportDataToSql(sqlTable, filePath, ',', true);
            System.IO.File.Delete(filePath);
            return new OkResult();
        }

        [HttpGet]
        [Route("dataset/{tableName}")]
        public IActionResult GetDataSet(string tableName, [FromQuery] char columnDelim, [FromQuery] string newLineChar)
        {
            var table = tablesDB.FindTable(tableName);
            string filePath = csvOutputHandler.MakeCSVFile(table, columnDelim);
            return File(filePath, "text/csv");
        }

        public IActionResult CreateNewDataSet([FromBody] JObject content)
        {
            var newTable = new SqlTable()
            {
                TableName = content["name"].ToString(),
                Coloumns = new Dictionary<string, string>()
            };

            foreach (var column in content["columns"])
                newTable.Coloumns[column["columnName"].ToString()] = column["types"].ToString();
            tablesDB.CreateTable(ref newTable);
            tablesDB.AddTableToRecords(newTable);
            return new OkResult();
        }
    }
}
