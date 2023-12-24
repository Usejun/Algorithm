using Algorithm;
using Algorithm.Technique;
using Algorithm.Text.JSON;
using Algorithm.Datastructure;
using Algorithm.Net;

namespace Algorithm
{
    internal class Program
    {       
        public static void Init()
        {
            System.Console.InputEncoding = System.Text.Encoding.Unicode;
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;
        }

        static void Main(string[] args)
        {
            Init();

            var text = @"
{
    ""name"": ""clear"",
    ""age"": 123,
    ""copy"": true
    ""obj"": {
        ""key"": ""!@#"",
        ""keys"": ""!@#@!#""
    }
}

";

            var json = JObject.Parse(text, JAccess.OnlyValue);

            json["age"].Update(123123);

            Util.Print(json["obj"]["key"].Access);            

        }
    }
}
    