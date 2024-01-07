using System;
using System.Net.Http;

using Algorithm.Text;

namespace Algorithm.Net
{
    public class Request
    {
        HttpClient client;

        public Request()
        {
            client = new HttpClient();
        }

        public string GetString(string url)
        {
            return client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        public StringBuffer GetStringBuffer(string url)
        {
            return new StringBuffer(client.GetAsync(url).Result.Content.ReadAsStringAsync().Result);
        }
    }
}
