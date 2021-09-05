using System;
using System.Collections.Generic;
using System.Data;

namespace simpletesttodelete
{
    public class DataTableCreater
    {
        public List<string> ColoumnsNames { get; set; }
        public List<string[]> TableDatas { get; set; }
        public string[] ColoumnsTypes { get; set; }
        public DataTable dataTable { get; set; }

        public DataTableCreater(List<string> coloumnsNames, List<string[]> tableDatas, string[] coloumnsTypes)
        {
            Console.WriteLine("hello");
            dataTable = new DataTable();
            ColoumnsNames = coloumnsNames;
            TableDatas = tableDatas;
            ColoumnsTypes = coloumnsTypes;
        }

        public void FilDataTable()
        {
            Console.WriteLine("1--------------------");

            CreateColoumns();
            AddDataToTable();
            Console.WriteLine("2--------------------");

        }

        public void CreateColoumns()
        {
            for (var i = 0; i < ColoumnsTypes.Length; i++)
            {
                Console.WriteLine(ColoumnsNames[i]);
                if (ColoumnsTypes[i].Equals("int"))
                {
                    DataColumn datecolumn = new DataColumn(ColoumnsNames[i], typeof(int));
                    datecolumn.AllowDBNull = true;
                    dataTable.Columns.Add(datecolumn);
                }
                else if (ColoumnsTypes[i].Equals("float"))
                {
                    DataColumn datecolumn = new DataColumn(ColoumnsNames[i], typeof(float));
                    datecolumn.AllowDBNull = true;
                    dataTable.Columns.Add(datecolumn);
                }
                else if (ColoumnsTypes[i].Equals("datetime"))
                {
                    DataColumn datecolumn = new DataColumn(ColoumnsNames[i], typeof(DateTime));
                    datecolumn.AllowDBNull = true;
                    dataTable.Columns.Add(datecolumn);
                }
                else if (ColoumnsTypes[i].Equals("nvarchar(50)"))
                {
                    DataColumn datecolumn = new DataColumn(ColoumnsNames[i], typeof(string));
                    datecolumn.AllowDBNull = true;
                    dataTable.Columns.Add(datecolumn);
                }
            }
        }

        public void AddDataToTable()
        {
            Console.WriteLine("3--------------------");
            Console.WriteLine(TableDatas.Count+"--------------------5");

            for (var i = 0; i < TableDatas.Count; i++)
            {
                Console.WriteLine("4--------------------");

                Console.WriteLine(i);
                DataRow addingRow = dataTable.NewRow();
                AddDataToRow(addingRow , TableDatas[i]);
                dataTable.Rows.Add(addingRow);  

            }
        }

        public void AddDataToRow(DataRow addingRow , string[] tableRow)
        {
            for (var i = 0; i < tableRow.Length; i++)
            {
                if (ColoumnsTypes[i].Equals("int"))
                {
                    if (tableRow[i].Equals(""))
                    {
                        addingRow[i] =  DBNull.Value;
                    }
                    else
                    {
                        addingRow[i] = Int16.Parse(tableRow[i]);
                    }
                    
                }
                else if (ColoumnsTypes[i].Equals("float"))
                {
                    if (tableRow[i].Equals(""))
                    {
                        addingRow[i] = DBNull.Value;
                    }
                    else
                    {
                        addingRow[i] = float.Parse(tableRow[i]);
                    }
                }
                else if (ColoumnsTypes[i].Equals("datetime"))
                {
                    if (tableRow[i].Equals(""))
                    {
                        addingRow[i] =  DBNull.Value;
                    }
                    else
                    {
                        addingRow[i] = DateTime.Parse(tableRow[i]);
                    }
                }
                else if (ColoumnsTypes[i].Equals("nvarchar(50)"))
                {
                    addingRow[i] = tableRow[i];
                }
                
            }
        }
        
    }
}