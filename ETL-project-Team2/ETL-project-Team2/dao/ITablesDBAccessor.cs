using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETL_project_Team2.models;

namespace ETL_project_Team2.dao
{
    public interface ITablesDBAccessor
    {
        void AddTable(string userName, SqlTable toBeAdded);
        SqlTable FindTable(string tableName, string userName);
    }
}
