using System.Data.SqlClient;
using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    public class AggregationService
    {
        private AggregationModel _aggregationModel;

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

        private string CreateQuery(SqlTable previousTable)
        {
            switch (_aggregationModel.AggregationType)
            {
                case AggregationEnum.Sum:
                    return "SELECT SUM(" + _aggregationModel.Columns +
                           ") FROM " + _aggregationModel.SqlTable.TableName + " ;";
                case AggregationEnum.Count:
                    return "SELECT COUNT(" + _aggregationModel.Columns +
                           ") FROM " + _aggregationModel.SqlTable.TableName + " ;";
                case AggregationEnum.Average:
                    return "SELECT AVERAGE(" + _aggregationModel.Columns +
                           ") FROM " + _aggregationModel.SqlTable.TableName + " ;";
                case AggregationEnum.Min:
                    return "SELECT MIN(" + _aggregationModel.Columns +
                           ") FROM " + _aggregationModel.SqlTable.TableName + " ;";
                default:
                    return "SELECT MAX(" + _aggregationModel.Columns +
                           ") FROM " + _aggregationModel.SqlTable.TableName + " ;";
            }
        }
    }
}