using Algorithm;
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

            JObject json = new JObject();


            json.Add("id", 123123)
                .Add("age", 10)
                .Add("sex", "male")
                .Add("lv", 0)
                .AddArray("skills", "attack", "magic", "sword")
                .AddObject("inventory")
                .AddObject("ability", ("str", 0), ("int", 0), ("dex", 0), ("luk", 0));

            json["inventory"].AddArray("item", "너덜너덜한 상의", "너덜너덜한 하의");

            Util.Print(json.ToJSON());


        }
    }
}
    