using Aggregation.Models;
using ETL_project_Team2.models;

namespace Aggregation.Controllers
{
    public class AggregationHandler : IAggregationHandler
    {
        private AggregationModel _aggregationModel;

        public SqlTable Operate(SqlTable table){
            _aggregationModel.SqlTable.Coloumns = table.Coloumns;
            _aggregationModel.SqlTable.DBConnection = table.DBConnection;

            var query = CreateQuery(table);
            _dbService.ExecuteNonQuery(_aggregationModel.SqlTable.DBConnection, query);
            
            return _aggreagtionModel.SqlTable;
        }
    }
    private string CreateQuery(SqlTable previousTable)
        {
            switch (_aggregationModel.AggregationType)
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