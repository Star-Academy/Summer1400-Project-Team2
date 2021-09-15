using System;
using System.Text.Json;
using ETL_project_Team2.controllers;
using ETL_project_Team2.dao;
using ETL_project_Team2.models;
using ETL_project_Team2.services;

namespace Aggregation.Controllers
{
    public class AggregationHandler : IAggregationHandler
    {
        private readonly AggregationService _aggregationService;
        private readonly IDBAccessor _dbAccessor;
        private AggregationModel _aggregationModel;

        public AggregationHandler(AggregationService aggregationService, IDBAccessor dbAccessor)
        {
            _aggregationService = aggregationService;
            _dbAccessor = dbAccessor;
        }

        public SqlTable Operate(SqlTable table)
        {
            _aggregationModel.SqlTable = new SqlTable()
            {
                DBConnection = table.DBConnection,
                Coloumns = table.Coloumns
            };
            _aggregationModel.AggregateQuery = _aggregationService.CreateQuery(table, _aggregationModel);
            _dbAccessor.ExecuteNonQuery("", _aggregationModel.AggregateQuery, _aggregationModel.SqlTable.DBConnection);
            return _aggregationModel.SqlTable;
        }

        public void SetParameters(string jsonString)
        {
            _aggregationModel = _aggregationService.CreateModel(jsonString);
        }
    }
}

