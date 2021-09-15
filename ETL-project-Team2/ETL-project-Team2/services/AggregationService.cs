using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json;
using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    public class AggregationService : IAggregateService
    {

        // void Aggregate(SqlConnection sqlConnection)
        // {
        // }
        //
        // public SqlTable Operate(SqlConnection sqlConnection, string aggregateQuery)
        // {
        //     _aggregationModel.SqlTable.Coloumns = table.Coloumns;
        //     _aggregationModel.SqlTable.DBConnection = table.DBConnection;
        //
        //     var query = CreateQuery(table, aggregateQuery);
        //     _dbService.ExecuteNonQuery(_aggregationModel.SqlTable.DBConnection, query);
        // }

        public string CreateQuery(SqlTable previousTable, AggregationModel aggregationModel)
        {
            switch (aggregationModel.AggregationType)
            {
                case AggregationEnum.Sum:
                    return "SELECT SUM(" + aggregationModel.Columns +
                           ") FROM " + aggregationModel.SqlTable.TableName + " ;";
                case AggregationEnum.Count:
                    return "SELECT COUNT(" + aggregationModel.Columns +
                           ") FROM " + aggregationModel.SqlTable.TableName + " ;";
                case AggregationEnum.Average:
                    return "SELECT AVERAGE(" + aggregationModel.Columns +
                           ") FROM " + aggregationModel.SqlTable.TableName + " ;";
                case AggregationEnum.Min:
                    return "SELECT MIN(" + aggregationModel.Columns +
                           ") FROM " + aggregationModel.SqlTable.TableName + " ;";
                default:
                    return "SELECT MAX(" + aggregationModel.Columns +
                           ") FROM " + aggregationModel.SqlTable.TableName + " ;";
            }
        }

        public AggregationModel CreateModel(string jsonString)
        {
            var temp = JsonSerializer.Deserialize<Temp1>(jsonString);
            return new AggregationModel
            {
                Columns = new List<string>() { temp.Parameters.Column },
                AggregationType = CreateAggregationType(temp.Parameters.AggregationType),
                ResultColumn = temp.Parameters.ResultColumn,
                TargetColumn = temp.Parameters.TargetColumn
            };
        }

        private AggregationEnum CreateAggregationType(string type)
        {
            if (type == "sum") return AggregationEnum.Sum;
            if (type == "min") return AggregationEnum.Min;
            if (type == "max") return AggregationEnum.Max;
            if (type == "average" || type == "avg") return AggregationEnum.Average;
            return AggregationEnum.Count;
        }
        
        internal class Temp1
        {
            public Temp2 Parameters { get; set; }
        }

        internal class Temp2
        {
            public string AggregationType{ get; set; }
            public string Column{ get; set; }
            public string groupByColumn{ get; set; }
            public string ResultColumn{ get; set; }
            public string TargetColumn{ get; set; }
        }
    }
}