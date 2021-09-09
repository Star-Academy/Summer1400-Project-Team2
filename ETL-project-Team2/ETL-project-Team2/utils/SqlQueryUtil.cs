using ETL_project_Team2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ETL_project_Team2.utils
{
    public class SqlQueryUtil
    {
        public string InsertIntoTargetTable(string query, SqlTable targetTable)
        {
            string resultQuery = "INSERT INTO {0} ({1})" + query;
            return string.Format(resultQuery, targetTable.TableName, SeperateColumnsByComma(targetTable));
        }

        public string CreateTable(SqlTable table)
        {
            string resultQuery = "CRAETE TABLE {0} ({1});";
            string columns = "";
            foreach (var columnPair in table.Coloumns)
                columns += columnPair.Key + ' ' + columnPair.Value + ", ";
            columns.TrimEnd(',', ' ');
            return string.Format(resultQuery, table.TableName, columns);
        }

        public string SeperateColumnsByComma(params SqlTable[] toBeSelected)
        {
            string selectedColumns = "";
            foreach (var table in toBeSelected)
                foreach (var column in table.Coloumns)
                    selectedColumns += '[' + table.TableName + "].[" + column + "], ";
            selectedColumns.TrimEnd(',', ' ');
            return selectedColumns;
        }

    }
}
