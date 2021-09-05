using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ETL_project_Team2.models
{
    public class SqlTable
    {
        public SqlConnection DBConnection { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, string> Coloumns { get; set; }
    }
}
