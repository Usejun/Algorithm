﻿using System;
using System.Linq;
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
            return new Json(json, Parse());
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

            throw new JSONParsingException("Type error");
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

                if (json[index] != ':') throw new JSONParsingException("Not object type");

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

            throw new JSONParsingException("Not number type");
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

            throw new JSONParsingException("Type error");
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
        List<(string Key, JObject JObj)> values;    
        
        public JObject()
        {
            values = new List<(string, JObject)>();
            valueType = ValueType.Object;
        }

        private JObject Get(string key)
        {
            foreach ((string _key, JObject jObj) in values)
                if (_key == key)
                    return jObj;

            throw new JSONIndexingException("It's not a key to existence");
        }

        private void Set(string key, JObject jObj)
        {
            if (values.Contains((key, jObj)))
                Update(key, jObj);
            else
                values.Add((key, jObj));
        }

        private void Update(string key, JObject jObj)
        {
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i].Key == key)
                {
                    values[i] = (key, jObj);
                    break;
                }
            }
        }
       
        public JObject this[object key]
        {
            get
            {
                if (valueType == ValueType.Object && key is string strKey)
                    return Get(strKey);
                else if (valueType == ValueType.Array && key is int intKey)
                    if (this is JArray jArray)
                        return jArray[intKey];

                throw new JSONIndexingException("It it not Indexable type");

            }
            set
            {
                if (valueType == ValueType.Object && key is string strKey)
                {
                    Set(strKey, value);
                    return;
                }
                else if (valueType == ValueType.Array && key is int intKey)
                    if (this is JArray jArray)
                    {
                        jArray[intKey] = value;
                        return;
                    }

                throw new JSONIndexingException("It it not Indexable type");
            }
        }

        public static implicit operator int(JObject jObject)
        {
            if (jObject.valueType == ValueType.Number)
                if (jObject is JNumber jNumber)
                    return (int)jNumber;

            throw new JSONConvertException("Not number type");
        }

        public static implicit operator long(JObject jObject)
        {
            if (jObject.valueType == ValueType.Number)
                if (jObject is JNumber jNumber)
                    return (long)jNumber;

            throw new JSONConvertException("Not number type");
        }

        public static implicit operator float(JObject jObject)
        {
            if (jObject.valueType == ValueType.Number)
                if (jObject is JNumber jNumber)
                    return (float)jNumber;

            throw new JSONConvertException("Not number type");
        }

        public static implicit operator double(JObject jObject)
        {
            if (jObject.valueType == ValueType.Number)
                if (jObject is JNumber jNumber)
                    return (double)jNumber;

            throw new JSONConvertException("Not number type");
        }

        public static implicit operator string(JObject jObject)
        {
            if (jObject.valueType == ValueType.String)
            {
                if (jObject is JString jString)
                    return (string)jString;
            }

            return jObject.ToString();
        }

        public static implicit operator bool(JObject jObject)
        {
            if (jObject.valueType == ValueType.Boolean)
                if (jObject is JBoolean jBoolean)
                    return (bool)jBoolean;

            throw new JSONConvertException("Not boolean type");
        }

        public static implicit operator List<JObject>(JObject jObject)
        {
            if (jObject.valueType == ValueType.String)
                if (jObject is JArray jArray)
                    return (List<JObject>)jArray;

            throw new JSONConvertException("Not array type");
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

        public static implicit operator int(JNumber jNumber) => (int)jNumber.value;
        public static implicit operator long(JNumber jNumber) => (long)jNumber.value;
        public static implicit operator float(JNumber jNumber) => (float)jNumber.value;
        public static implicit operator double(JNumber jNumber) => jNumber.value;
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

        public static implicit operator string(JString jString) => jString.value;
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

        public static implicit operator bool(JBoolean jBoolean) => jBoolean.value;
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

        private bool OutOfRange(int index)
        {
            if (index < 0 || index >= values.Count)
                return true;
            return false;
        }

        public override string ToString()
        {
            return string.Join(", ", values);
        }

        public JObject this[int index]
        {
            get
            {
                if (OutOfRange(index))
                    throw new JSONIndexingException("Out of range");

                return values[index];
            }
            set
            {
                if (OutOfRange(index))
                    throw new JSONIndexingException("Out of range");

                values[index] = value;
            }
        }

        public static implicit operator List<JObject>(JArray jArray) => jArray.values; 
    }

    public class JNull : JObject
    {
        public object Value => value;

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

    public class JSONParsingException : Exception
    {
        public JSONParsingException() { }
        public JSONParsingException(string message) : base(message) { }
        public JSONParsingException(string message, Exception inner) : base(message, inner) { }
        protected JSONParsingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class JSONConvertException : Exception
    {
        public JSONConvertException() { }
        public JSONConvertException(string message) : base(message) { }
        public JSONConvertException(string message, Exception inner) : base(message, inner) { }
        protected JSONConvertException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class JSONIndexingException : Exception
    {
        public JSONIndexingException() { }
        public JSONIndexingException(string message) : base(message) { }
        public JSONIndexingException(string message, Exception inner) : base(message, inner) { }
        protected JSONIndexingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}