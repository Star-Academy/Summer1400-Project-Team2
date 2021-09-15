using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    public interface IJoinService
    {
        public string MakeTargetTableQuery(SqlTable targetTable);
        SqlTable MakeTargetTable(SqlTable lTable, SqlTable rTable);
        string JoinQuery(JoinModel joinModel);
        JoinModel.Type GetJoinType(string type);
    }
}
