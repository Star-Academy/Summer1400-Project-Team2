using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_project_Team2.models
{
    public class JoinModel
    {
        public SqlTable LTable { get; set; }
        public SqlTable RTable { get; set; }
        public SqlTable TargetTable { get; set; }
        public string Condition { get; set; }
        public string Jointype { get; set; }
    }
}
