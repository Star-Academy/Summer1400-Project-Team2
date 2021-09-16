using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    public interface IDBService
    {
        string CreateTableQuery(SqlTable table);
        string CopyTableQuery(SqlTable source, SqlTable dest);
        string DropTableQuery(SqlTable table);
    }
}
