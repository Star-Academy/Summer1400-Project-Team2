using System.Data.SqlClient;
using System.IO;

namespace ETL_project_Team2.services
{
    public interface ICSVOutputHandler
    {
        public SqlDataReader CreateSqlDataReader(SqlConnection sqlCon);

        public void GetFromSql();

        public void ReadFromSql(SqlDataReader reader, object[] output, StreamWriter sw);
    }
}