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
        IActionResult OpertePipeline(int modelId, string userName, IPipelineDBAcessor pipelineDB);
        IActionResult UpdatePipeline(int modelId, string newContent, string userName, IPipelineDBAcessor pipelineDB);
        IActionResult GetPipeline(int modelId,string userName, IPipelineDBAcessor pipelineDB);
        IActionResult GetPreviewTable(int nodeId);
    }
}
