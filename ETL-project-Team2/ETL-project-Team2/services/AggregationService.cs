using System;
using System.Data.SqlClient;

namespace Aggregation.services
{
    public class AggregationService
    {

        void Aggregate(SqlConnection sqlConnection)
        {
            
        }

        public sqlTable Operate(sqlConnection sqlConnection, string aggregateQuery){

            _aggregationModel.SqlTable.Coloumns = table.Coloumns;
            _aggregationModel.SqlTable.DBConnection = table.DBConnection;

            var query = CreateQuery(table, aggregateQuery);
            _dbService.ExecuteNonQuery(_aggregationModel.SqlTable.DBConnection, query);
        }

        public string CreateQuery(SqlTable previousTable, AggregationModel aggregatitonModel)
        {
            switch (aggregationModel.AggregationType)
            {
                case Sum:
                    return "SELECT SUM(" +  _aggregationModel.Columns +
                    ") FROM " + _aggregationModel.SqlTable.TableName + " ;";
                case Count:
                    return "SELECT COUNT(" +  _aggregationModel.Columns +
                    ") FROM " + _aggregationModel.SqlTable.TableName + " ;";
                case Average:
                    return "SELECT AVERAGE(" +  _aggregationModel.Columns +
                    ") FROM " + _aggregationModel.SqlTable.TableName + " ;";
                case Min:
                    return "SELECT MIN(" +  _aggregationModel.Columns +
                    ") FROM " + _aggregationModel.SqlTable.TableName + " ;";
                case Max:
                    return "SELECT MAX(" +  _aggregationModel.Columns +
                    ") FROM " + _aggregationModel.SqlTable.TableName + " ;";
                
            }
        }
    }
}