using System;
using ETL_project_Team2.controllers;
using ETL_project_Team2.models;
using ETL_project_Team2.services;

namespace Aggregation.Controllers
{
    public class AggregationHandler : IAggregationHandler
    {
        private AggregationService AggregationService;

        public SqlTable Operate(SqlTable table, string AggregateQuery)
        {
            throw new NotImplementedException();
            // return ;
        }
    }
}

