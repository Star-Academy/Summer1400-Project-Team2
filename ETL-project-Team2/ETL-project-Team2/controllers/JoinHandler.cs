using ETL_project_Team2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETL_project_Team2.services;
using ETL_project_Team2.dao;
using Newtonsoft.Json.Linq;

namespace ETL_project_Team2.controllers
{
    public class JoinHandler : IOperation
    {
        private JoinModel _joinModel;
        private IJoinService _joinService;
        private IDBAccessor _dbService;
        private ITablesDBAccessor _tablesDBAccessor;

        public JoinHandler(IJoinService joinService, IDBAccessor dbService, 
            ITablesDBAccessor tablesDBAccessor)
        {
            _joinService = joinService;
            _dbService = dbService;
            _tablesDBAccessor = tablesDBAccessor;
            _joinModel = new JoinModel();
        }
        public SqlTable Operate(SqlTable table)
        {
            _joinModel.LTable = table;
            _joinModel.TargetTable = _joinService.MakeTargetTable(_joinModel.LTable, _joinModel.RTable);
            string createTragetTableQuery = _joinService.MakeTargetTableQuery(_joinModel.TargetTable);
            _dbService.ExecuteNonQuery("", createTragetTableQuery, table.DBConnection);
            string queryToBeExecuted = _joinService.JoinQuery(_joinModel);
            _dbService.ExecuteNonQuery("", queryToBeExecuted, table.DBConnection);
            return _joinModel.TargetTable;
        }

        public void SetParameters(string jsonString)
        {
            var parsedObj = JObject.Parse(jsonString);

            _joinModel.RTable = _tablesDBAccessor.FindTable((string)parsedObj["TableName"]);
            _joinModel.Jointype = _joinService.GetJoinType((string)parsedObj["JoinType"]);
            _joinModel.LTableColumn = (string)parsedObj["ColumnLTable"];
            _joinModel.RTableColumn = (string)parsedObj["ColumnRTable"];
        }
    }
}
