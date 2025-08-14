using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class Program6
    {
        public void p6()
        {
            Hashtable myHashtable = new Hashtable();
            myHashtable.Add("Apple", 1);
            myHashtable.Add("Banana", 2);
            myHashtable.Add("Cherry", 3);
            Console.WriteLine("Added key-value pairs.");
            Console.WriteLine();

            myHashtable.Remove("Banana");
            Console.WriteLine("Removed key 'Banana'.");
            Console.WriteLine();

            Console.WriteLine($"Contains key 'Apple': {myHashtable.ContainsKey("Apple")}");
            Console.WriteLine();

            Console.WriteLine($"Contains value '2': {myHashtable.ContainsValue(2)}");
            Console.WriteLine();

            myHashtable.Clear();
            Console.WriteLine("Cleared all key-value pairs.");
            Console.WriteLine();

  
            Console.WriteLine($"Number of items in the hashtable: {myHashtable.Count}");
        }
    }
}
