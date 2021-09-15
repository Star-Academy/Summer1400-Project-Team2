using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_project_Team2.models
{
    public class JoinModel
    {
        public class Type
        {
            private Type(string type) { Value = type; }
            public string Value { get; private set; }
            public static Type Inner { get { return new Type("INNER"); } }
            public static Type Outer { get { return new Type("OUTER"); } }
            public static Type Left { get { return new Type("LEFT"); } }
            public static Type Rigth { get { return new Type ("RIGHT"); } }
        }
        public SqlTable LTable { get; set; }
        public SqlTable RTable { get; set; }
        public SqlTable TargetTable { get; set; }
        public string RTableColumn { get; set; }
        public string LTableColumn { get; set; }
        public Type Jointype { get; set; }
    }
}
