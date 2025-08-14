using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Program7
    {
        interface IShape
       
        {
            void CalCircle(double r);
            void CalTriangle(double b, double h);
            void CalSquare(double b, double l);
        }

        class Area : IShape
        {
            public void CalCircle(double r) { Console.WriteLine($"Area of Circle = {3.14 * (r * r)}"); }
            public void CalTriangle(double b, double h) { Console.WriteLine($"Area of Triangle = {0.5 * b * h}"); }
            public void CalSquare(double b, double l) { Console.WriteLine($"Area of Square   = {l * b }"); }

        }



        public void P7()
        {
            Area area = new Area();
            area.CalCircle(10);
            area.CalTriangle(5, 10);
            area.CalSquare(5, 10);

        }


    }
}
