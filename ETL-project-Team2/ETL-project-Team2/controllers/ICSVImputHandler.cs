namespace DefaultNamespace
{
    public interface ICSVImputHandler
    {
        public void CreateTableName();

        public void GetDataTabletFromCsvFile();
        public void CreateTable(bool isOriginal);
        public void ImportDataInSql(bool isOriginal);


        public SqlConnection CreateSqlConnection();

        public string CreateSqlCommand(bool isOriginal);
    }
}