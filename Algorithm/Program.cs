using Algorithm;
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

        static void Main()
        {
            Init();

            List<string> q = new List<string>();

            q.AddRange("hello", "my", "name", "is", "good");

            Util.Print();
            Util.Print(q);
        }    
    }
}
    