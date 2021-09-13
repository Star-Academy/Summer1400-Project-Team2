namespace Aggregation.Models
{
    public class SqlTable
    {
        public SqlConnection DBConnection { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, string> Coloumns { get; set; }
    }
}