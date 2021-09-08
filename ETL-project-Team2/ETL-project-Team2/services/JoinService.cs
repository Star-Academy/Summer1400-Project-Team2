using ETL_project_Team2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETL_project_Team2.utils;
using System.Data.SqlClient;

namespace ETL_project_Team2.services
{
    public class JoinService : IJoinService
    {
        private readonly string query =
            "SELECT {0}\n" +
            "FROM {1}\n" +
            "{2} JOIN {3}\n" +
            "ON {4}";

        public string JoinQuery(JoinModel joinModel)
        {
            var queryUtil = new SqlQueryUtil();
            string joinQuery = string.Format(query, queryUtil.SeperateColumnsByComma(joinModel.LTable),
                                        joinModel.LTable.TableName, joinModel.Jointype.Value,
                                        joinModel.RTable.TableName, MakeCondition(joinModel));
            return queryUtil.InsertIntoTargetTable(joinQuery, joinModel.TargetTable);
        }

        public SqlTable MakeTargetTable(SqlTable lTable, SqlTable rTable)
        {
            if (lTable.DBConnection.ConnectionString != rTable.DBConnection.ConnectionString)
                throw new ArgumentException();
            var targetTable = new SqlTable()
            {
                DBConnection = new SqlConnection(lTable.DBConnection.ConnectionString),
                TableName = lTable.TableName + rTable.TableName,
                Coloumns = new Dictionary<string, string>()
            };

            foreach (var column in lTable.Coloumns)
                targetTable.Coloumns[lTable.TableName + '.' + column.Key] = column.Value;
            foreach (var column in rTable.Coloumns)
                targetTable.Coloumns[rTable.TableName + '.' + column.Key] = column.Value;
            return targetTable;
        }

        private string MakeCondition(JoinModel joinModel)
        {
            if (!joinModel.LTable.Coloumns.ContainsKey(joinModel.LTableColumn))
                throw new Exception(joinModel.LTable.TableName + "doesn't contain " + joinModel.LTableColumn);
            if (!joinModel.RTable.Coloumns.ContainsKey(joinModel.RTableColumn))
                throw new Exception(joinModel.LTable.TableName + "doesn't contain " + joinModel.RTableColumn);

            return joinModel.LTable.TableName + '.' + joinModel.LTableColumn +
                '=' +
                joinModel.RTable.TableName + '.' + joinModel.RTableColumn;
        }
    }
}
