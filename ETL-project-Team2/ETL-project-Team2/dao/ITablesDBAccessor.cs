using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETL_project_Team2.models;

namespace ETL_project_Team2.dao
{
    public interface ITablesDBAccessor
    {
        void AddUserDataBase();
        void InitTableList();
        void AddTableToRecords(SqlTable toBeAdded);
        void CreateTable(ref SqlTable toBeCreated);
        SqlTable FindTable(string tableName);
        List<string> FetchDataSetsList();
    }
}
