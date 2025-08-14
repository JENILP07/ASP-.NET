using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class Program2
    {
        public void p2()
        {
           List<string> studentName = new List<string>();  
            studentName.Add("Jenil");
            studentName.Add("krizz");
            studentName.Add("jeel");
            studentName.Add("brind");

            foreach (string i in studentName)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();
            Console.WriteLine("Index Range");
            studentName.RemoveRange(1, 2);

            foreach (string i in studentName)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine();
            Console.WriteLine("Index");
            studentName.RemoveAt(1);
            foreach (string i in studentName)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine();
            Console.WriteLine("Clear");
            studentName.Clear();
        }
    }
}
