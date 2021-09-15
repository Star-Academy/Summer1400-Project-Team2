using System.Data.SqlClient;
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
            throw new System.NotImplementedException();
        }
    }
}