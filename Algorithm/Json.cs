using Algorithm.DataStructure;

namespace Algorithm.JSON
{
    public enum ValueType
    {
        Number,
        String,
        Boolean,
        Array,
        Null,
        Object
    }    

    public class JsonParser
    {
        int len;
        int index;
        string json;

        public JsonParser(string json) 
        {
            this.json = json;
            len = json.Length;
            index = 0;
        }

        public Json GetJson()
        {
            return new Json(json, (JObject)Parse());
        }

        JObject Parse()
        {
            SkipWhitespace();

            if (json[index] == '{') return ParseObject();
            else if (json[index] == '[') return ParseArray();
            else if (json[index] == '"') return ParseString();
            else if (char.IsDigit(json[index])) return ParseNumber();
            else if (json[index] == 't' || json[index] == 'f') return ParseBoolean();
            else if (json[index] == 'n') return ParseNull();

            return new JObject();
        }

        void SkipWhitespace()
        {
            while (index < len && char.IsWhiteSpace(json[index])) index++;
        }

        JObject ParseObject()
        {
            JObject obj = new JObject();
            index++;

            while (json[index] != '}')
            {
                SkipWhitespace();
                JString jString = ParseString();
                string key = jString.Value;
                SkipWhitespace();

                if (json[index] != ':') return obj;

                index++;
                SkipWhitespace();

                JObject value = Parse();
                obj[key] = value;

                SkipWhitespace();
                if (json[index] == ',') index++;
            }

            index++;
            return obj;
        }

        JNumber ParseNumber()
        {
            int start = index;
            while (char.IsDigit(json[index]) || json[index] == '.') index++;

            string numberString = json.Substring(start, index - start);

            if (double.TryParse(numberString, out double result))
            {
                return new JNumber(result);
            }

            return null;
        }

        JString ParseString()
        {
            index++;
            int start = index;
            while (json[index] != '"') index++;            

            JString jString = new JString(json.Substring(start, index - start));
            index++;

            return jString;
        }

        JArray ParseArray()
        {
            JArray array = new JArray(new List<JObject>());
            index++;

            while (json[index] != ']')
            {
                SkipWhitespace();
                JObject value = Parse();
                array.Values.Add(value);

                SkipWhitespace();
                if (json[index] == ',') index++;
            }

            index++;
            return array;
        }

        JBoolean ParseBoolean()
        {
            if (json.Substring(index, 4) == "true")
            {
                index += 4;
                return new JBoolean(true);
            }
            else
            {
                index += 5;
                return new JBoolean(false);
            }
        }

        JNull ParseNull()
        {
            if (json.Substring(index, 4) == "null")
            {
                index += 4;
                return new JNull();
            }

            return null;
        }
    }

    public class Json
    {
        readonly string json;
        JObject jObject;

        public Json(string json, JObject jObject)
        {
            this.json = json;
            this.jObject = jObject;
        }

        public JObject this[string key]
        {
            get => jObject[key];
            set => jObject[key] = value;
        }

        public override string ToString()
        {
            return json;
        }

        public static Json Parse(string json)
        {
            JsonParser parser = new JsonParser(json);
            
            return parser.GetJson();
        }
    }

    public class JObject
    {
        protected ValueType valueType;
        Dictionary<string, JObject> values;    
        
        public JObject()
        {
            values = new Dictionary<string, JObject>();
            valueType = ValueType.Object;
        }

        public JObject this[string key]
        {
            get => values[key];
            set => values[key] = value; 
        }

        public override string ToString()
        {
            return string.Join("\n ", values.ToPairs());
        }
    }

    public class JNumber : JObject
    {
        public double Value => value;

        double value;

        public JNumber(double value)
        {
            this.value = value;
            valueType = ValueType.Number;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }

    public class JString : JObject
    {
        public string Value => value;

        string value;

        public JString(string value)
        {
            this.value = value;
            valueType = ValueType.String;
        }

        public override string ToString()
        {
            return value;
        }
    }

    public class JBoolean : JObject
    {
        public bool Value => value;

        bool value;

        public JBoolean(bool value)
        {
            this.value = value;
            valueType = ValueType.Boolean;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }

    public class JArray : JObject
    {
        public List<JObject> Values => values;

        List<JObject> values;

        public JArray(List<JObject> values)
        {
            this.values = values;
            valueType = ValueType.Array;
        }

        public override string ToString()
        {
            return string.Join("\n", values);
        }
    }

    public class JNull : JObject
    {
        public object Value => null;

        object value;

        public JNull()
        {
            value = null;
            valueType = ValueType.Null;
        }

        public override string ToString()
        {
            return "null";
        }
    }

}
