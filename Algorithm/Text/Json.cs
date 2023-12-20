using System;
using Algorithm.Datastructure;

namespace Algorithm.Text.JSON
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

    public class JObject
    {
        private class JsonParser
        {
            private readonly int len;
            private int index;
            private readonly string json;

            public JsonParser(string json)
            {
                this.json = json ?? throw new JSONNullException();
                len = json.Length;
                index = 0;
            }

            public JObject GetJObject()
            {
                return Parse();
            }

            private JObject Parse()
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

            private void SkipWhitespace()
            {
                while (index < len && char.IsWhiteSpace(json[index])) index++;
            }

            private JObject ParseObject()
            {
                JObject obj = new JObject();
                index++;

                while (json[index] != '}')
                {
                    SkipWhitespace();
                    JString jString = ParseString();
                    string key = jString.Value;
                    SkipWhitespace();

                    if (json[index] != ':')
                    {
                        throw new JSONParsingException("Not object type");
                    }
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

            private JNumber ParseNumber()
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

            private JString ParseString()
            {
                index++;
                int start = index;
                while (!(json[index] == '"' && json[index - 1] != '\\'))
                    index++;

                string text = json.Substring(start, index - start);
                string convertedText = ConvertUnicode(text);

                JString jString = new JString(convertedText);
                index++;

                return jString;
            }

            private JArray ParseArray()
            {
                JArray array = new JArray();
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

            private JBoolean ParseBoolean()
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

            private JNull ParseNull()
            {
                if (json.Substring(index, 4) == "null")
                {
                    index += 4;
                    return new JNull();
                }

                throw new JSONParsingException("Type error");
            }

            private string ConvertUnicode(string text)
            {
                StringBuffer convertedText = new StringBuffer();
                int textIndex = 0;

                while (textIndex < text.Length)
                {
                    if (text[textIndex] == '\\' && text[textIndex + 1] == 'u')
                    {
                        convertedText.Append(Convert.ToChar(int.Parse(text.Substring(textIndex + 2, 4), System.Globalization.NumberStyles.HexNumber)));
                        textIndex += 6;
                    }
                    else
                    {
                        convertedText.Append(text[textIndex]);
                        textIndex++;
                    }
                }

                return convertedText.ToString();
            }
        }

        protected const int DEFAULTTEXTHEIGHT = 2;

        public ValueType ValueType => valueType;

        protected ValueType valueType;
        private readonly List<(string Key, JObject JObj)> values;    
        
        public JObject()
        {
            values = new List<(string, JObject)>();
            valueType = ValueType.Object;
        }

        public JObject this[object key]
        {
            get
            {
                if (valueType == ValueType.Object && key is string strKey)
                {
                    foreach ((string _key, JObject jObj) in values)
                        if (_key == strKey)
                            return jObj;

                    throw new JSONIndexingException("It's not a key to existence");
                }
                else if (valueType == ValueType.Array && key is int intKey)
                    if (this is JArray jArray)
                        return jArray[intKey];

                throw new JSONIndexingException("It's not Indexable type");

            }
            set
            {
                if (valueType == ValueType.Object && key is string strKey)
                {
                    if (values.Contains((strKey, value)))
                    {
                        for (int i = 0; i < values.Count; i++)
                        {
                            if (values[i].Key == strKey)
                            {
                                values[i] = (strKey, value);
                                break;
                            }
                        }
                    }
                    else
                    {
                        values.Add((strKey, value));
                    }
                    return;
                }
                else if (valueType == ValueType.Array && key is int intKey)
                    if (this is JArray jArray)
                    {
                        jArray[intKey] = value;
                        return;
                    }

                throw new JSONIndexingException("It's not Indexable type");
            }
        }

        public JObject AddObject(string key)
        {
            values.Add((key, new JObject()));

            return this;
        }

        public JObject AddObject(string key, params (string key, object value)[] _values)
        {
            JObject jObject = new JObject();

            foreach ((string _key, object value) in _values)
                jObject.Add(_key, value);

            values.Add((key, jObject));

            return this;
        }

        public JObject AddArray(string key)
        {
            values.Add((key, new JArray()));

            return this;
        }

        public JObject AddArray(string key, params object[] valuse)
        {
            List<JObject> list = new List<JObject>();

            foreach (object value in valuse)
            {
                if (value is double d)
                    list.Add(new JNumber(d));
                else if (value is int i)
                    list.Add(new JNumber(i));
                else if (value is long l)
                    list.Add(new JNumber(l));
                else if (value is float f)
                    list.Add(new JNumber(f));
                else if (value is string s)
                    list.Add(new JString(s));
                else if (value is bool b)
                    list.Add(new JBoolean(b));
                else if (value is null)
                    list.Add(new JNull());
                else
                    list.Add(new JObject());
            }

            values.Add((key, new JArray(list)));

            return this;
        }

        public virtual JObject Add(object value)
        {
            if (valueType == ValueType.Array && this is JArray jArray)
            {
                if (value is double d)
                    jArray.Add(new JNumber(d));
                else if (value is int i)
                    jArray.Add(new JNumber(i));
                else if (value is long l)
                    jArray.Add(new JNumber(l));
                else if (value is float f)
                    jArray.Add(new JNumber(f));
                else if (value is string s)
                    jArray.Add(new JString(s));
                else if (value is bool b)
                    jArray.Add(new JBoolean(b));
                else if (value is null)
                    jArray.Add(new JNull());
                else
                    jArray.Add(new JObject());
            }

            return this;
        }

        public JObject Add(string key, object value)
        {
            if (valueType == ValueType.Object)
            {
                if (value is double d)
                    values.Add((key, new JNumber(d)));
                else if (value is int i)
                    values.Add((key, new JNumber(i)));
                else if (value is long l)
                    values.Add((key, new JNumber(l)));
                else if (value is float f)
                    values.Add((key, new JNumber(f)));
                else if (value is string s)
                    values.Add((key, new JString(s)));
                else if (value is bool b)
                    values.Add((key, new JBoolean(b)));
                else if (value is null)
                    values.Add((key, new JNull()));
                else if (value is List<JObject> li)
                    values.Add((key, new JArray(li)));
                else
                    AddObject(key);
            }

            return this;
        }

        public virtual string ToJSON()
        {
            StringBuffer sb = new StringBuffer();

            Put(this, 0);

            return sb.ToString();

            void Put(JObject jObject, int depth)
            {
                sb.AppendLine("{");
                for (int i = 0; i < jObject.values.Count; i++)
                {
                    (string key, JObject jObj) = jObject.values[i];

                    sb.Append(' ', (depth + 1) * DEFAULTTEXTHEIGHT);
                    sb.Append($"\"{key}\": ");

                    if (jObj.valueType == ValueType.Object)
                        Put(jObj, depth + 1);
                    else if (jObj.valueType == ValueType.Array)
                        sb.Append(jObj.ToJSON(depth + 1));
                    else
                        sb.Append(jObj.ToJSON());

                    if (i != jObject.values.Count - 1)
                        sb.Append(", ");
                    sb.Append('\n');
                }

                sb.Append(' ', depth * DEFAULTTEXTHEIGHT);
                sb.Append('}');
            }
        }

        public virtual string ToJSON(int height)
        {
            StringBuffer sb = new StringBuffer();

            Put(this, height);

            return sb.ToString();

            void Put(JObject jObject, int depth)
            {
                sb.AppendLine("{");                
                for (int i = 0; i < jObject.values.Count; i++)
                {
                    (string key, JObject jObj) = jObject.values[i];

                    sb.Append(' ', (depth + 1) * DEFAULTTEXTHEIGHT);
                    sb.Append($"\"{key}\": ");

                    if (jObj.valueType == ValueType.Object)
                        Put(jObj, depth + 1);
                    else if (jObj.valueType == ValueType.Array)
                        sb.Append(jObj.ToJSON(depth + 1));
                    else
                        sb.Append(jObj.ToJSON());

                    if (i != jObject.values.Count - 1)
                        sb.Append(", ");
                    sb.Append('\n');
                }

                sb.Append(' ', depth * DEFAULTTEXTHEIGHT);
                sb.Append('}');
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

        public override string ToString()
        {
            return ToJSON();
        } 

        public static JObject Parse(string json)
        {
            return new JsonParser(json).GetJObject();
        }
    }

    public class JNumber : JObject
    {
        public double Value => value;

        private readonly double value;

        public JNumber(double value)
        {
            this.value = value;
            valueType = ValueType.Number;
        }

        public override string ToJSON()
        {
            return $"{value}";
        }

        public override string ToJSON(int height)
        {
            return ToJSON(); 
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

        private readonly string value;

        public JString(string value)
        {
            this.value = value;
            valueType = ValueType.String;
        }

        public override string ToJSON()
        {
            return $"\"{value}\"";
        }

        public override string ToJSON(int height)
        {
            return $"\"{value}\"";
        }

        public static implicit operator string(JString jString) => $"{jString.value}";
    }

    public class JBoolean : JObject
    {
        public bool Value => value;

        private readonly bool value;

        public JBoolean(bool value)
        {
            this.value = value;
            valueType = ValueType.Boolean;
        }

        public override string ToJSON()
        {
            return value ? "true" : "false";
        }

        public override string ToJSON(int height)
        {
            return ToJSON();
        }

        public override string ToString()
        {
            return ToJSON();
        }

        public static implicit operator bool(JBoolean jBoolean) => jBoolean.value;
    }

    public class JArray : JObject
    {
        public List<JObject> Values => values;

        private readonly List<JObject> values;

        public JArray()
        {
            values = new List<JObject>();
            valueType = ValueType.Array;
        }

        public JArray(List<JObject> values)
        {
            this.values = values;
            valueType = ValueType.Array;
        }

        public override JObject Add(object value)
        {
            if (value is double d)
                values.Add(new JNumber(d));
            else if (value is int i)
                values.Add(new JNumber(i));
            else if (value is long l)
                values.Add(new JNumber(l));
            else if (value is float f)
                values.Add(new JNumber(f));
            else if (value is string s)
                values.Add(new JString(s));
            else if (value is bool b)
                values.Add(new JBoolean(b));
            else if (value is null)
                values.Add(new JNull());
            else if (value is List<JObject> li)
                values.Add(new JArray(li));
            else
                values.Add(new JObject());

            return this;
        }

        private bool OutOfRange(int index)
        {
            if (index < 0 || index >= values.Count)
                return true;
            return false;
        }

        public override string ToJSON()
        {
            StringBuffer sb = new StringBuffer();

            sb.Append("[");
            if (values.Count != 0) sb.Append('\n');
            for (int i = 0; i < values.Count; i++)
            {
                sb.Append(' ', DEFAULTTEXTHEIGHT);
                sb.Append(values[i].ToJSON());
                if (i != values.Count - 1)
                    sb.Append(',');
                sb.Append('\n');
            }

            sb.Append(']');

            return sb.ToString();
        }

        public override string ToJSON(int height)
        {
            StringBuffer sb = new StringBuffer();

            sb.Append("[");
            if (values.Count != 0) sb.Append('\n');
            for (int i = 0; i < values.Count; i++)
            {
                sb.Append(' ', (height + 1) * DEFAULTTEXTHEIGHT);

                if (values[i].ValueType == ValueType.Object ||
                    values[i].ValueType == ValueType.Array)
                    sb.Append(values[i].ToJSON(height + 1));
                else
                    sb.Append(values[i].ToJSON());

                if (i != values.Count - 1)
                    sb.Append(',');

                sb.Append('\n');
            }

            if (values.Count != 0) sb.Append(' ', height * DEFAULTTEXTHEIGHT);
            sb.Append(']');

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToJSON();            
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
        public object Value => null;

        public JNull()
        {
            valueType = ValueType.Null;
        }

        public override string ToJSON()
        {
            return "null";
        }

        public override string ToJSON(int height)
        {
            return "null";
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

    public class JSONNullException : Exception
    {
        public JSONNullException() { }
        public JSONNullException(string message) : base(message) { }
        public JSONNullException(string message, Exception inner) : base(message, inner) { }
        protected JSONNullException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
