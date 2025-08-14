using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Program2
    {
        public void p2() {
            try {
                int[] nums = new int[5];
                for (int i = 0; i < 6; i++)
                {
                    Console.Write(i+1 + " = ");
                    nums[i] =  Convert.ToInt32(Console.ReadLine()); 
                }
            }
            catch(Exception e) {
                Console.WriteLine(e);
            }
        }
    }
}
