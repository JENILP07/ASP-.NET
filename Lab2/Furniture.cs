using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal class Furniture
    {
        public string Material;
        public double Price;
        public void DisplayFurnitureDetails()
        {
            Console.WriteLine($"Material: {Material}");
            Console.WriteLine($"Price: {Price:C}");
        }
    }
    internal class Table : Furniture
    {
        public double Height;
        public double SurfaceArea;
        public void DisplayTableDetails()
        {
            Console.WriteLine($"Height: {Height} meters");
            Console.WriteLine($"Surface Area: {SurfaceArea} square meters");
        }
    }
}
