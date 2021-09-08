using System

namespace ETL_project_Team2.models
{
    enum AggregationEnum{
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
        public list<string> Columns; { get; set; }
        public string ResultColumn; { get; set; }
        public string TargetColumn; { get; set; }
        public AggregationEnum AggregationType; { get; set; }

        public AggregationModel(SqlTable sqlTable, string AggregateQuery)
        {
            this.AggregateQuery = AggregateQuery;
            this.SqlTable = sqlTable;
        }
    }
}