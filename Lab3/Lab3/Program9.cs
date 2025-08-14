using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class Program9
    {
        public void p9()
        {
            Console.Write("Enter a sentence: ");
            string input = Console.ReadLine();

            string[] words = input.Split(' '); 

            string longestWord = "";
            int maxLength = 0;

            foreach (string word in words)
            {
                if (word.Length > maxLength)
                {
                    maxLength = word.Length;
                    longestWord = word;
                }
            }

            Console.WriteLine("Longest word: " + longestWord);
        }
    }
}
