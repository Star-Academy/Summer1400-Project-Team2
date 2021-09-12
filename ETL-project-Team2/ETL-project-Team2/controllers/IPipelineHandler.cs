using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ETL_project_Team2.services;
using ETL_project_Team2.dao;

namespace ETL_project_Team2.controllers
{
    public interface IPipelineHandler
    {
        IActionResult OperatePipeline(int modelId, IPipelineDBAcessor pipelineDB);
        IActionResult UpdatePipeline(int modelId, string newContent, IPipelineDBAcessor pipelineDB);
        IActionResult GetPipeline(int modelId, IPipelineDBAcessor pipelineDB);
        IActionResult GetPreviewTable(int nodeId);
    }
}
