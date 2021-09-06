using System;
using System.Data.SqlClient;

namespace ETL_project_Team2.services
{
    public class FilterService : IFilterService
    {
        private readonly IYmlFilterParser _ymlFilterParser;
        private readonly IDBService _dbService;
        
        public FilterService(IYmlFilterParser ymlFilterParser, IDBService dbService)
        {
            this._ymlFilterParser = ymlFilterParser;
            _dbService = dbService;
        }
        
        public void Filter(string query, string currentDbName, string destinationDbName, SqlConnection sqlConnection)
        {
            var command = MakeQuery(query, currentDbName, destinationDbName);
            // var sqlCommand = new SqlCommand(command, sqlConnection);
            // sqlCommand.ExecuteNonQuery();
            _dbService.ExecuteNonQuery()
        }

        private string MakeQuery(string query, string currentDbName, string destinationDbName)
        {
            var result = "SELECT * INTO ";
            result += destinationDbName;
            result += " FROM " + currentDbName;
            result += " WHERE " + _ymlFilterParser.ParseQuery(query) + " ;";
            return result;
        }
    }
}