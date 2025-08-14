using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Program10
    {

        public void p10()
        {
            Console.Write("Enter Value 1 : ");
            int x = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Value 2 : ");
            int y = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Value 3 : ");
            int z = Convert.ToInt32(Console.ReadLine());

            int ans = (x > y) ? ((x > z) ? x : z) : (y > z) ? y : z;
            Console.WriteLine(ans);
        }
    }

}
