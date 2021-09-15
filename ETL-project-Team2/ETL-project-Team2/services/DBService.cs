using ETL_project_Team2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_project_Team2.services
{
    public class DBService : IDBService
    {
        public string CopyTableQuery(SqlTable source, SqlTable dest)
        {
            return $"INSERT INTO [{dest.TableName}]\n"
                + $"SELECT * FROM [{source.TableName}];";
        }

        public string DopTableQuery(SqlTable table)
        {
            return $"DROP TABLE [{table.TableName}];";
        }
    }
}
