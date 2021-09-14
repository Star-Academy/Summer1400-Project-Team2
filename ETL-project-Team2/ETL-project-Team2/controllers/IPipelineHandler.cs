using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ETL_project_Team2.services;
using ETL_project_Team2.dao;
using Newtonsoft.Json.Linq;

namespace ETL_project_Team2.controllers
{
    public interface IPipelineHandler
    {
        IActionResult OperatePipeline(int modelId, IPipelineDBAcessor pipelineDB, ITablesDBAccessor tablesDB);
        IActionResult SetNodeParams(int modelId, JObject content, IPipelineDBAcessor pipelineDB, ITablesDBAccessor tablesDB);
        IActionResult UpdatePipeline(int modelId, JObject content, IPipelineDBAcessor pipelineDB, ITablesDBAccessor tablesDB);
        IActionResult GetPipeline(int modelId, IPipelineDBAcessor pipelineDB);
        IActionResult GetPreviewTable(int nodeId);
    }
}
