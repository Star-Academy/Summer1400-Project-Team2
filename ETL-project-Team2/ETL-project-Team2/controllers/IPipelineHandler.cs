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
        IActionResult CreateNewPipeline(JObject content);
        IActionResult GetPipelineName(int modelId);
        IActionResult EditPipelineName(int modelId, JObject content);
        IActionResult OperatePipeline(int modelId);
        IActionResult SetNodeParams(int modelId, JObject content);
        IActionResult UpdatePipeline(int modelId, JObject content);
        IActionResult GetPipeline(int modelId);
        IActionResult GetPreviewTable(int nodeId);
    }
}
