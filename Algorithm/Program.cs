using Algorithm.Sort;
using Algorithm.Text;
using Algorithm.Technique;
using Algorithm.Text.JSON;
using Algorithm.Datastructure;

using static Algorithm.Util;
using static Algorithm.Datastructure.Extensions;
using System.Net.Http;
using System.IO;
using System;

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

            var client = new HttpClient();

            var content = client.GetAsync("https://api.jikan.moe/v4/anime?q=mushouku&sfw").Result.Content;
            var stream = content.ReadAsStreamAsync().Result;
            Json json;
            using (StreamReader sr = new StreamReader(stream))
            {
                var t = sr.ReadToEnd();

                
                //Print(t);


                json = Json.Parse(t);
                Print(json);
            }
        }
    }
}
    