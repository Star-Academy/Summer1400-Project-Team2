using System;

namespace simpletesttodelete
{
    public class TypeDefinder
    {
        public string TypeOfValue(string inputValue)
        {
            if (TryConvertTo<int>(inputValue))
            {
                return "int";
            }
            else if (TryConvertTo<double>(inputValue))
            {
                return "float";
            }
            else if (TryConvertTo<DateTime>(inputValue))
            {
                return "datetime";
            }
            else
            {
                return "char(50)";
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