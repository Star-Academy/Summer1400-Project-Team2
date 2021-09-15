using System.Collections.Generic;

namespace ETL_project_Team2.models
{
    public enum AggregationEnum{
        Count,
        Sum,
        Average,
        Min,
        Max
    }


    public class AggregationModel
    {
        public SqlTable SqlTable { get; set; }
        public string AggregateQuery { get; set; }
        public List<string> Columns { get; set; }
        public string ResultColumn { get; set; }
        public string TargetColumn { get; set; }
        public AggregationEnum AggregationType { get; set; }

        public AggregationModel(SqlTable sqlTable, string aggregateQuery)
        {

            this.AggregateQuery = aggregateQuery;
            this.SqlTable = sqlTable;
        }
    }
}