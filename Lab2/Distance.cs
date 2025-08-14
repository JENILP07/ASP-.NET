using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal class Distance
    {
        int dist1;
        int dist2;
        int dist3;
        public Distance(int dist1, int dist2)
        {
            this.dist1 = dist1;
            this.dist2 = dist2;
        }
        public void calculateAddition()
        {
            dist3 = dist1+ dist2;
        }
        public void DisplayDistances()
        {
            Console.WriteLine($"Distance 1: {dist1}");
            Console.WriteLine($"Distance 2: {dist2}");
            Console.WriteLine($"Addition of distances (Distance 3): {dist3}");
        }
    }
}
