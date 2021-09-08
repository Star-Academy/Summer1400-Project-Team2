using ETL_project_Team2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETL_project_Team2.services;
using Newtonsoft.Json.Linq;

namespace ETL_project_Team2.controllers
{
    public class JoinHandler : IOperation
    {
        private JoinModel joinModel;
        private IJoinService joinService;
        private IDBService dbService;
        private ITableService tableService;

        public JoinHandler(IJoinService joinService, IDBService dbService, ITableService tableService)
        {
            this.joinService = joinService;
            this.dbService = dbService;
            this.tableService = tableService;
            joinModel = new JoinModel();
        }
        public SqlTable Operate(SqlTable table)
        {
            joinModel.LTable = table;
            joinModel.TargetTable = joinService.MakeTargetTable(joinModel.LTable, joinModel.RTable);
            string queryToBeExecuted = joinService.JoinQuery(joinModel);
            dbService.ExecuteNonQuery(joinModel.TargetTable.DBConnection, queryToBeExecuted);
            return joinModel.TargetTable;
        }

        public void SetParameters(string jsonString)
        {
            var parsedObj = JObject.Parse(jsonString);

            joinModel.RTable = tableService.FindTable((string)parsedObj["TableName"]);
            joinModel.Jointype = GetType((string)parsedObj["JoinType"]);
            joinModel.LTableColumn = (string)parsedObj["ColumnLTable"];
            joinModel.RTableColumn = (string)parsedObj["ColumnRTable"];
        }

        private JoinModel.Type GetType(string type)
        {
            string lowerCaseType = type.ToLower();
            switch (lowerCaseType)
            {
                case ("inner"):
                    return JoinModel.Type.Inner;
                case ("outer"):
                    return JoinModel.Type.Outer;
                case ("left"):
                    return JoinModel.Type.Left;
                case ("right"):
                    return JoinModel.Type.Rigth;
            }
            throw new ArgumentException(type + "is not a join type");
        }
    }
}
