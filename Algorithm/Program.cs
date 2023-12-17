using Algorithm.Sort;
using Algorithm.Text;
using Algorithm.Technique;
using Algorithm.Text.JSON;
using Algorithm.Datastructure;

using static Algorithm.Util;
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

        static void Main(string[] args)
        {
            Init();

            string text = @"{""widget"": {
    ""debug"": ""on"",
    ""window"": {
        ""title"": ""Sample Konfabulator Widget"",
        ""name"": ""main_window"",
        ""width"": 500,
        ""height"": 500
    },
    ""image"": { 
        ""src"": ""Images/Sun.png"",
        ""name"": ""sun1"",
        ""hOffset"": 250,
        ""vOffset"": 250,
        ""alignment"": ""center""
    },
    ""text"": {
        ""data"": ""Click Here"",
        ""size"": 36,
        ""style"": ""bold"",
        ""name"": ""text1"",
        ""hOffset"": 250,
        ""vOffset"": 100,
        ""alignment"": ""center"",
        ""onMouseUp"": ""sun1.opacity = (sun1.opacity / 100) * 90;""
    }
}}    
";

            Json JSON = Json.Parse(text);

            Print(JSON);

            

        }
    }
}
    