using Algorithm.JSON;
using Algorithm.Technique;
using Algorithm.DataStructure;
using static Algorithm.Util;
using static Algorithm.DataStructure.Extensions;

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
            var json = "{\r\n  \"product\": \"Live JSON generator\",\r\n  \"version\": 3.1,\r\n  \"releaseDate\": \"2014-06-25T00:00:00.000Z\",\r\n  \"demo\": true,\r\n  \"person\": {\r\n    \"id\": 12345,\r\n    \"name\": \"John Doe\",\r\n    \"phones\": {\r\n      \"home\": \"800-123-4567\",\r\n      \"mobile\": \"877-123-1234\"\r\n    },\r\n    \"email\": [\r\n      \"jd@example.com\",\r\n      \"jd@example.org\"\r\n    ],\r\n    \"dateOfBirth\": \"1980-01-02T00:00:00.000Z\",\r\n    \"registered\": true,\r\n    \"emergencyContacts\": [\r\n      {\r\n        \"name\": \"Jane Doe\",\r\n        \"phone\": \"888-555-1212\",\r\n        \"relationship\": \"spouse\"\r\n      },\r\n      {\r\n        \"name\": \"Justin Doe\",\r\n        \"phone\": \"877-123-1212\",\r\n        \"relationship\": \"parent\"\r\n      }\r\n    ]\r\n  }\r\n}";

            Json JSON = Json.Parse(json);

            Print(JSON["person"]["email"][1]);
            
        }
    }
}
    