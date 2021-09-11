using ETL_project_Team2.models;
using ETL_project_Team2.services;
using ETL_project_Team2.dao;

namespace ETL_project_Team2.controllers
{
    public class FilterHandler : IFilterHandler, IOperation
    {
        private readonly IDBAccessor _dbService;
        private FilterModel _filterModel;
        private readonly IFilterService _filterService;
        public FilterHandler(IDBAccessor dbService, IFilterService filterService)
        {
            _dbService = dbService;
            _filterService = filterService;
        }

        public SqlTable Operate(SqlTable table, string userName)
        {
            _filterModel.CreatedTable.Coloumns = table.Coloumns;
            _filterModel.CreatedTable.DBConnection = table.DBConnection;

            var query = CreateQueryFromConditionQuery(table);
            _dbService.ExecuteNonQuery(query, _filterModel.CreatedTable.DBConnection);
            
            return _filterModel.CreatedTable;
        }

        private string CreateQueryFromConditionQuery(SqlTable previousTable)
        {
            return "SELECT * FROM " + previousTable.TableName +
                   " INTO " + _filterModel.CreatedTable.TableName +
                   " WHERE " + _filterModel.ConditionQuery + " ;";
        }

        public void SetParameters(string tree, string userName)
        {
            _filterModel = _filterService.GetFilterModel(tree);
        }
    }
}