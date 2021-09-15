using System;
using System.Text.Json;
using ETL_project_Team2.controllers;
using ETL_project_Team2.models;
using ETL_project_Team2.services;

namespace Aggregation.Controllers
{
    public class AggregationHandler : IAggregationHandler
    {
        private readonly AggregationService _aggregationService;
        private AggregationModel _aggregationModel;

        public AggregationHandler(AggregationService aggregationService)
        {
            _aggregationService = aggregationService;
        }

        public SqlTable Operate(SqlTable table)
        {
            throw new NotImplementedException();
        }

        public void SetParameters(string jsonString)
        {
            _aggregationModel = _aggregationService.CreateModel(jsonString);
        }
    }
}

