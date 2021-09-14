using System;

namespace ETL_project_Team2.services
{
    public class TypeDefinder
    {
        public string TypeOfValue(string inputValue)
        {
            if (TryConvertTo<int>(inputValue))
            {
                return "INT";
            }
            else if (TryConvertTo<double>(inputValue))
            {
                return "FLOAT";
            }
            else if (TryConvertTo<DateTime>(inputValue))
            {
                return "DATETIME";
            }
            else
            {
                return "NVARCHAR(MAX)";
            }
        }

        public bool TryConvertTo<T>(string inputValue)
        {
            Object result = null;
            try
            {
                result = Convert.ChangeType(inputValue, typeof(T));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}