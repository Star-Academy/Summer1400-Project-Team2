using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    public class FilterService : IFilterService
    {

        public FilterModel GetFilterModel(string postFixTree)
        {
            var conditionQuery = GetConditionQueryFromTree(postFixTree);
            var sqlTable = new SqlTable
            {
                TableName = "#Temp_" + new Random().Next(1_000_000_000)
            };
            return new FilterModel(sqlTable, conditionQuery);
        }

        // remove public todo
        public string GetConditionQueryFromTree(string tree)
        {
            if (StringContainsSqlInjection(tree)) throw new ArgumentException();
            var queries = new List<string>();
            var charArray = tree.ToCharArray();
            for (var i = 0; i < charArray.Length; i++)
            {
                if (charArray[i] == '|')
                {
                    ReduceQueriesWithOr(queries);
                }
                else if (charArray[i] == '&')
                {
                    ReduceQueriesWithAnd(queries);
                }
                else if (charArray[i] == '(')
                {
                    i = AddQueryToQueries(i, charArray, queries);
                }
            }

            if (queries.Count != 1) throw new ArgumentException();
            return queries[0];
        }

        private int AddQueryToQueries(int index, char[] chars, List<string> queries)
        {
            var query = "";
            while (chars[++index] != ')') query += chars[index];
            queries.Add(query);
            return index;
        }

        private void ReduceQueriesWithOr(List<string> queries)
        {
            ReduceQueries(queries, "OR");
        }

        private void ReduceQueriesWithAnd(List<string> queries)
        {
            ReduceQueries(queries, "AND");
        }

        private void ReduceQueries(List<string> queries, string logic)
        {
            if (queries.Count == 1) throw new ArgumentException();
            var tempQuery = CombineQueries(queries[^2], queries[^1], logic);
            queries.RemoveAt(queries.Count - 1);
            queries[^1] = tempQuery;
        }

        private string CombineQueries(string firstQuery, string secondQuery, string logic)
        {
            return "( " + firstQuery + " " + logic + " " + secondQuery + " )";
        }

        private bool StringContainsSqlInjection(string query)
        {
            Regex.Replace(query, "'[.]+'", "");
            var injections = new List<string>()
            {
                "SELECT", "DELETE", "1=1", "INTO", "FROM"
            };
            return injections.Any(query.Contains);
        }
    }
}