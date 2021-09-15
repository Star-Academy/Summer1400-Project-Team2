using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_project_Team2.models
{
    public class FilterModel
    {
        public SqlTable CreatedTable { get; set; }
        public string ConditionQuery { get; set; }

        public FilterModel(SqlTable createdTable, string conditionQuery)
        {
            this.ConditionQuery = conditionQuery;
            this.CreatedTable = createdTable;
        }
    }
}