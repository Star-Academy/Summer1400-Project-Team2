using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using ETL_project_Team2.models;

namespace ETL_project_Team2.services
{
    class CSVInputService : ICSVInputService
    {
        public static int TableId { get; set; }

        public string ConnectionString { get; set; }

        //  public List<string> ColoumnsNames { get; set; }
        public List<string[]> TableDatas { get; set; }
        //    public string[] ColoumnsTypes { get; set; }

        public CSVInputService()
        {
            // ColoumnsNames = new List<string>();
            TableDatas = new List<string[]>();
            TableId += 1;
        }

        public Dictionary<string, string> GetDataTableFromCsvFile(string filePath, string delimeter)
        {
            List<string> ColoumnsNames = new List<string>();
            string[] ColoumnsTypes;
            using (TextFieldParser fileReader = new TextFieldParser(filePath))
            {
                fileReader.SetDelimiters(new string[] {delimeter});
                fileReader.HasFieldsEnclosedInQuotes = true;
                string[] colFields = fileReader.ReadFields();
                foreach (string column in colFields)
                {
                    ColoumnsNames.Add(new string(column));
                }

                TypeDefinder typeDefinder = new TypeDefinder();
                ColoumnsTypes = new string[ColoumnsNames.Count];
                while (!fileReader.EndOfData)
                {
                    string[] fieldData = fileReader.ReadFields();
                    TableDatas.Add(fieldData);

                    for (int i = 0; i < fieldData.Length; i++)
                    {
                        ColoumnsTypes[i] = typeDefinder.TypeOfValue(fieldData[i]);
                    }
                }
            }

            return CreateDictionary(ColoumnsNames, ColoumnsTypes);
        }

        public Dictionary<string, string> CreateDictionary(List<string> coloumnsNames, string[] coloumnsTypes)
        {
            Dictionary<string, string> coloumnsData = new Dictionary<string, string>();
            for (var i = 0; i < coloumnsNames.Count; i++)
            {
                coloumnsData.Add(coloumnsNames[i], coloumnsTypes[i]);
            }

            return coloumnsData;
        }

        public void ImportDataToSql(SqlTable table)
        {
            string sqlCommandStart = $"INSERT INTO {table.TableName} (";

            for (var i = 0; i < ColoumnsNames.Count - 1; i++)
            {
                sqlCommandStart += ColoumnsNames[i].TrimEnd(new char[] {','}) + ",";
            }

            sqlCommandStart += ColoumnsNames[ColoumnsNames.Count - 1] + ") VALUES (";

            foreach (var tableData in TableDatas)
            {
                string sqlCommand = "";
                sqlCommand += sqlCommandStart;
                for (var i = 0; i < tableData.Length - 1; i++)
                {
                    sqlCommand += "'" + tableData[i] + "' , ";
                }

                sqlCommand += "'" + tableData[tableData.Length - 1] + "')";
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand())
                    {
                        comm.Connection = conn;
                        comm.CommandText = sqlCommand;

                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}