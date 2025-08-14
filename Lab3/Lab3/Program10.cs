using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class Program10
    {
        public void p10()
        {

            Console.Write("Enter a Character: ");
            char input = Convert.ToChar(Console.Read());
            char result;

            if (char.IsLower(input))
            {
                 result = char.ToUpper(input);
            }
            else
            {
                 result = char.ToLower(input);
            }

            

            Console.WriteLine("Result: " + result);
        }
    }
}
