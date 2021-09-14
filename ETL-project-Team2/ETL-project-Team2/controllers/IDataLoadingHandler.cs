using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ETL_project_Team2.dao;
using ETL_project_Team2.services;

namespace ETL_project_Team2.controllers
{
    public interface IDataLoadingHandler
    {
        IActionResult LoadListOfPipelines();
        IActionResult LoadListtOfDataSets();
        public IActionResult PutCSVFileOnDB();
    }
}
