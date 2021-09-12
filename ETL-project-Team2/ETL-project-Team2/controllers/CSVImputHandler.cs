using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace simpletesttodelete
{
    class CSVImputHandler : ICSVImputHandler
    {
        public static int TableId { get; set; }
        public string ConnectionString { get; set; }
        public List<string> ColoumnsNames { get; set; }
        public List<string[]> TableDatas { get; set; }
        public string[] ColoumnsTypes { get; set; }
        public string FilePath { get; set; }
        public string Delimeter { get; set; }
        public string OriginalTableName { get; set; }
        public string CopyTableName { get; set; }


        public CSVImputHandler(string filePath, string delimeter)
        {
            ColoumnsNames = new List<string>();
            TableDatas = new List<string[]>();
            FilePath = filePath;
            Delimeter = delimeter;
            TableId += 1;
            CreateTableName();
        }


        public void StartCreatingTables()
        {
            this.GetDataTabletFromCsvFile();
            this.CreateTable(true);
            this.ImportDataInSql(true);
            this.CreateTable(false);
            this.ImportDataInSql(false);
        }

      

        public void CreateTableName()
        {
            OriginalTableName = "TableNo" + TableId;
            CopyTableName = OriginalTableName + "Copy";
        }

        public void GetDataTabletFromCsvFile()
        {
            try
            {
                using (TextFieldParser fileReader = new TextFieldParser(FilePath))
                {
                    fileReader.SetDelimiters(new string[] {Delimeter});
                    fileReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = fileReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        ColoumnsNames.Add(string.Copy(column));
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
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }


        public void CreateTable(bool isOriginal)
        {
            CreateSqlConnection();
            string connectionString = ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sqlCommand = CreateSqlCommand(isOriginal);
                    using (SqlCommand command = new SqlCommand(sqlCommand, con))
                        command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void ImportDataInSql(bool isOriginal)
        {
            string
                sqlCommandStart;
            if (isOriginal)
            {
                sqlCommandStart = "INSERT INTO " + OriginalTableName + " (";
            }
            else
            {
                sqlCommandStart = "INSERT INTO " + CopyTableName + " (";
            }

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

                        try
                        {
                            conn.Open();
                            comm.ExecuteNonQuery();
                        }
                        catch (SqlException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
        }


        public SqlConnection CreateSqlConnection()
        {
            ConnectionString = @"Server=" + "." + ";Database=" + "CSVTEST" + ";Trusted_Connection=True";
            return new SqlConnection(ConnectionString);
        }

        public string CreateSqlCommand(bool isOriginal)
        {
            string sql;
            if (isOriginal)
            {
                sql = "Create Table " + OriginalTableName + "(";
            }
            else
            {
                sql = "Create Table " + CopyTableName + "(";
            }


            for (var i = 0; i < ColoumnsNames.Count; i++)
            {
                sql += ColoumnsNames[i].TrimEnd(new char[] {','}) + " " + ColoumnsTypes[i] + ",";
            }

            sql = sql.TrimEnd(new char[] {Delimeter[0]}) + ");";
            return sql;
        }
    }
}