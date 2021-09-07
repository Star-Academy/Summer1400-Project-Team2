using ETL_project_Team2.models;
using ETL_project_Team2.services;

namespace ETL_project_Team2.controllers
{
    public class FilterHandler : IFilterHandler
    {
        private readonly IDBService _dbService;
        private FilterModel _filterModel;
        private readonly IFilterService _filterService;
        public FilterHandler(IDBService dbService, IFilterService filterService)
        {
            _dbService = dbService;
            _filterService = filterService;
        }

        public SqlTable Operate(SqlTable table)
        {
            _filterModel.CreatedTable.Coloumns = table.Coloumns;
            _filterModel.CreatedTable.DBConnection = table.DBConnection;

            var query = CreateQueryFromConditionQuery(table);
            _dbService.ExecuteNonQuery(_filterModel.CreatedTable.DBConnection, query);
            
            return _filterModel.CreatedTable;
        }

        private string CreateQueryFromConditionQuery(SqlTable previousTable)
        {
            return "SELECT * FROM " + previousTable.TableName +
                   " INTO " + _filterModel.CreatedTable.TableName +
                   " WHERE " + _filterModel.ConditionQuery + " ;";
        }

        public void SetParameters(string tree)
        {
            _filterModel = _filterService.GetFilterModel(tree);
        }
    }
}