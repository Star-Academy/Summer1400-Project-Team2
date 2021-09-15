﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    class CSVInputService : ICSVInputService
    {
        public Dictionary<string, string> GetColumnTypesAndNames(string filePath, char delim)
        {
            var result = new Dictionary<string, string>();
            var fileReader = new StreamReader(filePath);
            string headerLine = fileReader.ReadLine();
            fileReader.Dispose();

            string[] columnNames = headerLine.Split(delim);

            for (int i = 0; i < columnNames.Length; ++i)
            {
                result.Add(columnNames[i], "NVARCHAR(MAX)");
            }
            return result;
        }

        public void ImportDataToSql(SqlTable table, string filePath, char delim, bool hasHeader)
        {
            using(table.DBConnection)
            {
                var file = new StreamReader(filePath);
                string commandString = $"INSERT INTO {table.TableName} ({SeperateColumns(table.Coloumns)})\n" +
                    "VALUES ({0});";
                string line = string.Empty;
                if (hasHeader)
                    file.ReadLine();
                while((line = file.ReadLine()) != null)
                {
                    table.DBConnection.Open();
                    string processedLine = line.Replace(delim, ',');
                    processedLine = processedLine.Replace(",,", ",null ,");
                    using (var sqlCommand = new SqlCommand(string.Format(commandString, processedLine), table.DBConnection))
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        private string SeperateColumns(Dictionary<string,string> columns)
        {
            var columnsNames = columns.Select(x => x.Key).ToList();
            string result = string.Empty;
            foreach (var name in columnsNames)
                result += name + ", ";
            result = result.TrimEnd(' ', ',');
            return result;
        }
    }
}