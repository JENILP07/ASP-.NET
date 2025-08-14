using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program7
    {
        public void p7()
        {
            
            int[] numbers = { 1, 2,1,1,2,2,2 };

            ArrayList uniqueNumbers = new ArrayList();
            ArrayList frequencies = new ArrayList();

            foreach (int i in numbers)
            {
                int index = uniqueNumbers.IndexOf(i);
                if (index != -1)
                {
                    frequencies[index] = (int)frequencies[index] + 1;
                }
                else
                {                  
                    uniqueNumbers.Add(i);
                    frequencies.Add(1);
                }
            }

           
            for (int i = 0; i < uniqueNumbers.Count; i++)
            {
                Console.WriteLine($"Number: {uniqueNumbers[i]}, Frequency: {frequencies[i]}");
            }
        }


    }
}
