using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class Program4
    {
        public void p4()
        {
            Queue<int> q = new Queue<int>();
            q.Enqueue(1);
            q.Enqueue(2);                
            q.Enqueue(3);   
            q.Enqueue(4);
            q.Enqueue(5);   
            q.Enqueue(6);
            q.Enqueue(7);

            foreach (int i in q)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine();
            Console.WriteLine(q.Dequeue());

            Console.WriteLine();
            Console.WriteLine(q.Peek());

            Console.WriteLine();
            Console.WriteLine(q.Contains(1));

            q.Clear();

        } 
    }
}
