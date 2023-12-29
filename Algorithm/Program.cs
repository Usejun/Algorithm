﻿using Algorithm;
using Algorithm.Net;
using Algorithm.Technique;
using Algorithm.Text.JSON;
using Algorithm.Datastructure;

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

            var t = @"
{
    ""name"": ""key"",
    ""value"": 123,
    ""array"": [
        ""hi"",
        ""bye""
        ]
    ""obj"": {
        ""key"": ""kkk"",
        ""val"": true
        }
}";

            var json = JObject.Parse(t);

            Util.Print(json["obj"]);
        }
    }
}
    