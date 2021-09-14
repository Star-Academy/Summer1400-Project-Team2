using ETL_project_Team2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace ETL_project_Team2.services
{
    public class CSVOutputHandler : ICSVOutputHandler
    {
        private const string tempFilePath = "F:";

        public string MakeCSVFile(SqlTable table, char delim)
        {
            string filePath = $"{tempFilePath}{Path.DirectorySeparatorChar}{table.TableName}TempFile.csv";
            var fileStream = File.CreateText(filePath);
            fileStream.WriteLine(MakeCSVHeader(table.Coloumns, delim));
            using (table.DBConnection)
            {
                string commandString = $"SELECT * FROM {table.TableName};";
                var sqlCommand = new SqlCommand(commandString, table.DBConnection);

                table.DBConnection.Open();
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    string currentLine = string.Empty;
                    foreach(var columnPair in table.Coloumns)
                    {
                        currentLine += reader[columnPair.Key].ToString() + delim + ' ';
                    }
                    currentLine = currentLine.TrimEnd(' ', delim);
                    fileStream.WriteLine(currentLine);
                }

                sqlCommand.Dispose();
                reader.Close();
            }
            fileStream.Close();
            return filePath;
        }

        private string MakeCSVHeader(Dictionary<string, string> columns, char delim)
        {
            var columnNames = columns.Select(x => x.Key).ToList();
            string result = string.Empty;
            foreach (string name in columnNames)
                result += name + delim + ' ';
            result = result.TrimEnd(' ', delim);
            result += '\n';
            return result;
        }
    }    
}
