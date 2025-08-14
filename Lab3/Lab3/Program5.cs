using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Program5
    {
        public void p5()
        {
         


                // Original string
                string original = "Hello, C# World!";
                Console.WriteLine($"Original String = {original}");

                // 1. Length
                Console.WriteLine($"Length of string: {original.Length}");

                // 2. ToUpper and ToLower
                Console.WriteLine($"Uppercase: {original.ToUpper()}");
                Console.WriteLine($"Lowercase: {original.ToLower()}");

                // 3. Contains
                Console.WriteLine($"Contains 'C#': {original.Contains("C#")}");

                // 4. IndexOf
                Console.WriteLine($"Index of 'World': {original.IndexOf("World")}");

                // 5. Substring
                Console.WriteLine($"Substring starting from index 7: {original.Substring(7)}");

                // 6. Replace
                Console.WriteLine($"Replace 'World' with 'Universe': {original.Replace("World", "Universe")}");

                // 7. Split
                string[] words = original.Split(' ');
                Console.WriteLine("Words in string:");
                foreach (string word in words)
                {
                    Console.WriteLine(word);
                }

                // 8. Trim
                string paddedString = "               HI String   ";
                Console.WriteLine($"Trimmed: '{paddedString.Trim()}'");
    
        }
    }
}
