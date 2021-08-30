using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

namespace ETL_project_Team2.services
{
    public class YmlFilterParser : IYmlFilterParser
    {
        public string ParseQuery(string query)
        {
            var ymlDeserialized = Des(query);
            var dict = (Dictionary<object, object>) ymlDeserialized;
            return dict.ContainsKey("AND") ? MakeAnd(dict["AND"]) : MakeOr(dict["OR"]);
        }
        
        private object Des(string yml)
        {
            var d = new Deserializer();
            return d.Deserialize<object>(yml);
        }

        private string MakeAnd(object obj)
        {
            var results = GetResults(obj);

            return "( " + results.Aggregate((curr, next) => curr + " AND " + next) + " )";
        }

        private string MakeOr(object obj)
        {
            var results = GetResults(obj);

            return "( " + results.Aggregate((curr, next) => curr + " OR " + next) + " )";
        }

        private List<string> GetResults(object obj)
        {
            var list = (List<object>)obj;

            return list.Cast<Dictionary<object, object>>()
                .Select(temporaryDict => ExtractAnswer(temporaryDict))
                .ToList();
        }

        private string ExtractAnswer(Dictionary<object, object> dictionary)
        {
            if (dictionary.ContainsKey("OR"))
            {
                return MakeOr(dictionary["OR"]);
            }
            else if (dictionary.ContainsKey("AND"))
            {
                return MakeAnd(dictionary["AND"]);
            }
            else
            {
                return MakeQuery(dictionary["q"]);
            }
        }

        private string MakeQuery(object obj)
        {
            return (string)obj;
        }
    }
}