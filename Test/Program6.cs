using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program6
    {
        abstract class Shape
        {
        

            public virtual void CalculateArea(string name,double r)
            {
                
                Console.WriteLine($"Area = {3.14 * (r * r)}");
            }
            public virtual void CalculateArea(string name, double l,double b)
            {
                
                Console.WriteLine($"Area = {l*b}");
            }
        }

        class Circle : Shape
        {
            public override void CalculateArea(string name, double r)
            {
                base.CalculateArea(name, r);
            }
        }

        class Reactangle : Shape
        {
            public override void CalculateArea(string name, double l,double b)
            {
                base.CalculateArea(name,l,b );
            }
        }

        public void p6()
        {
            Reactangle reactangle = new Reactangle();
            reactangle.CalculateArea("Circle", 5);

            Circle circle = new Circle();
            circle.CalculateArea("Reactangle", 5, 10);
        }
    }
}
