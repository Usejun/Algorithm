using System;
using System.Collections.Generic;
using System.Text;
using Algorithm;

namespace Algorithm
{   
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            var pq = new DataStructure.PriorityQueue<string, int>(true);            

            pq.Enqueue("개구리", 0);
            pq.Enqueue("@!#!@#", 213);
            pq.Enqueue("!@#!@#", 2222);

            if (pq.ContainsKey("개구리"))
            {
                Utility.Print("개구리가 있잖아!");
            }

            Utility.Print(pq.Dequeue());
            Utility.Print(pq.Dequeue());
            Utility.Print(pq.Dequeue());
        }
    }
}
