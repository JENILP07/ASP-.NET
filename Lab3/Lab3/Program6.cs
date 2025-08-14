using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class Program6
    {
        public void p6()
        {
            
                Console.Write("Enter a string: ");
                string input = Console.ReadLine();

                string result = ReplaceCase(input);

                Console.WriteLine("Result: " + result);
            }

            static string ReplaceCase(string str)
            {
                char[] arr = str.ToCharArray();

                for (int i = 0; i < arr.Length; i++)
                {
                    if (char.IsLower(arr[i]))
                        arr[i] = char.ToUpper(arr[i]);
                    else if (char.IsUpper(arr[i]))
                        arr[i] = char.ToLower(arr[i]);
                }

                return new string(arr);
            }
        
    }
}
