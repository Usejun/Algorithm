using System;
using System.Net.Http;

using Algorithm.Text;

namespace Algorithm.Net
{
    public class Request
    {
        HttpClient client;

        string url;

        public Request()
        {
            client = new HttpClient();
        }

        public Request(string url)
        {
            client = new HttpClient();
            this.url = url;
        }

        public string GetString()
        {
            if (url == null)
                throw new RequestException("URL is Empty");

            return client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        public string GetString(string url)
        {
            return client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        public StringBuffer GetStringBuffer()
        {
            if (url == null)
                throw new RequestException("URL is Empty");

            return new StringBuffer(client.GetAsync(url).Result.Content.ReadAsStringAsync().Result);
        }

        public StringBuffer GetStringBuffer(string url)
        {
            return new StringBuffer(client.GetAsync(url).Result.Content.ReadAsStringAsync().Result);
        }
    }

    public class RequestException : Exception
    {
        public RequestException() : base() { }
        public RequestException(string message) : base(message) { }
        public RequestException(string message, Exception inner) : base(message, inner) { }
        protected RequestException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
