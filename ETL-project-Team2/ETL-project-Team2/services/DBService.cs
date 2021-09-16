using ETL_project_Team2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_project_Team2.services
{
    public class DBService : IDBService
    {
        public string CreateTableQuery(SqlTable table)
        {
            string columns = string.Empty;
            foreach (var columnPair in table.Coloumns)
                columns += columnPair.Key + ' ' + columnPair.Value + ", ";
            columns = columns.TrimEnd(' ', ',');
            return $"CREATE TABLE {table.TableName} ({columns});";
        }
        public string CopyTableQuery(SqlTable source, SqlTable dest)
        {
            return $"INSERT INTO [{dest.TableName}]\n"
                + $"SELECT * FROM [{source.TableName}];";
        }

        public string DropTableQuery(SqlTable table)
        {
            return $"DROP TABLE [{table.TableName}];";
        }
    }
}
