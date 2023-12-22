using Algorithm;
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

            Request rq = new Request();

            var json = JObject.Parse(rq.GetString("https://api.jikan.moe/v4/anime?q=Mushoku tensei&sfw"));

            Util.Print(json);



        }
    }
}
    