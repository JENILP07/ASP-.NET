using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class Program5
    {
        public void p5() {

            Dictionary<string, int> myDictionary = new Dictionary<string, int>();

            myDictionary.Add("Apple", 1);
            myDictionary.Add("Banana", 2);
            myDictionary.Add("Cherry", 3);
            Console.WriteLine("Added key-value pairs.");
            Console.WriteLine();

            myDictionary.Remove("Banana");
            Console.WriteLine("Removed key 'Banana'.");
            Console.WriteLine();

            Console.WriteLine($"Contains key 'Apple': {myDictionary.ContainsKey("Apple")}");
            Console.WriteLine();

            Console.WriteLine($"Contains value '2': {myDictionary.ContainsValue(2)}");
            Console.WriteLine();

            myDictionary.Clear();
            Console.WriteLine("Cleared all key-value pairs.");
            Console.WriteLine();

            Console.WriteLine($"Number of items in the dictionary: {myDictionary.Count}");
            Console.WriteLine();
        }
    }
}
