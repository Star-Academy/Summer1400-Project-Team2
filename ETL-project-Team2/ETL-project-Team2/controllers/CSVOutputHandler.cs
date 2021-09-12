using System;
using System.Data.SqlClient;
using System.IO;
using ETL_project_Team2.services;

namespace testconverttocsv
{
    class CSVOutputHandler:ICSVOutputHandler
    {
        public string ConnectionString { get; set; }
        public string FileNameToCreate { get; set; }
        public string CopyTableName { get; set; }
        public string Delimeter { get; set; }


        public CSVOutputHandler(string connectionString, string fileNameToCreate, string copyTableName,
            string delimeter)
        {
            ConnectionString = connectionString;
            FileNameToCreate = fileNameToCreate;
            CopyTableName = copyTableName;
            Delimeter = delimeter;
        }


        public SqlDataReader CreateSqlDataReader(SqlConnection sqlCon)
        {
            SqlCommand sqlCmd = new SqlCommand("Select * from " + CopyTableName, sqlCon);
            return sqlCmd.ExecuteReader();
        }

        public void GetFromSql()
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            sqlCon.Open();
            SqlDataReader reader = CreateSqlDataReader(sqlCon);
            StreamWriter sw = new StreamWriter(FileNameToCreate);
            object[] output = new object[reader.FieldCount];
            ReadFromSql(reader, output, sw);
            sw.Close();
            reader.Close();
            sqlCon.Close();
        }

        public void ReadFromSql(SqlDataReader reader, object[] output, StreamWriter sw)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                output[i] = reader.GetName(i);

            sw.WriteLine(string.Join(Delimeter, output));

            while (reader.Read())
            {
                reader.GetValues(output);
                sw.WriteLine(string.Join(Delimeter, output));
            }
        }

     
    }
}