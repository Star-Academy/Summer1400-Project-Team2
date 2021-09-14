using System;
using Microsoft.AspNetCore.Mvc;

namespace ETL_project_Team2.controllers
{
    [Route("/[controller]/[action]")]
    public class ExceptionController : Controller
    {
        [HttpGet]
        public IActionResult ExceptionThrown()
        {
            return BadRequest();
        }
        
    }
}