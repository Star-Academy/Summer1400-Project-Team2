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
        IActionResult OpertePipeline(int modelId);
        IActionResult UpdatePipeline(string jsonString);
        IActionResult GetPipeline(int modelId, IPipelineDBAcessor pipelineDB);
        IActionResult GetPreviewTable(int nodeId);
    }
}
