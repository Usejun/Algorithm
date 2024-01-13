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

            BST<int, int> bst = new BST<int, int>();

            bst.Add(1, 2);
            bst.Add(2, 3);
            bst.Add(3, 4);
            bst.Add(4, 5);
            bst.Add(5, 6);
            bst.Add(6, 7);

            Util.Print(bst.FindRoute(6, 7));

        }    
    }
}
    