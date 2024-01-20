using Algorithm;
using Algorithm.Net;
using Algorithm.Technique;
using Algorithm.Text.JSON;
using Algorithm.Datastructure;

using static Algorithm.Datastructure.Extensions;

namespace Algorithm
{
    internal class Program
    {       
        public static void Init()
        {
            System.Console.InputEncoding = System.Text.Encoding.Unicode;
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;
        }

        static void Main()
        {
            Init();

            string text = @"
{
  ""name"": ""Usejun"",
  ""age"": 888,
  ""height"": 11111111,
  ""skill"": {
    ""Psychokinesis"": false,
    ""Prognosis"": false
  },
  ""test"": [
    1,
    2,
    3
  ]
}";

            JObject json = JObject.Parse(text);

            Util.Print(json);

        }    
    }
}
    