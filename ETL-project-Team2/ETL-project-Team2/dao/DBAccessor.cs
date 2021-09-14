using ETL_project_Team2.models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ETL_project_Team2.dao
{
    public class DBAccessor : IDBAccessor
    {
        private static readonly ConcurrentBag<KeyValuePair<string, SqlCommand>> Commands =
            new ConcurrentBag<KeyValuePair<string, SqlCommand>>();
        public int ExecuteNonQuery(string cancellationToken, string queryCommand, SqlConnection dbConnection)
        {
            using (var sqlCommand = new SqlCommand(queryCommand, dbConnection))
            {
                var keyValuePair = new KeyValuePair<string, SqlCommand>(cancellationToken, sqlCommand);
                Commands.Add(keyValuePair);
                dbConnection.Open();
                var temp = sqlCommand.ExecuteNonQuery();
                Commands.TryTake(out keyValuePair);
                return temp;
            }
        }

        public void CancelExecution(string cancellationToken)
        {
            foreach (var pair in Commands)
            {
                if (pair.Key != cancellationToken) continue;
                pair.Value.Cancel();
                
                return;
            }

            throw new Exception("SqlCommand Already finished Executing.");
        }
    }
}
