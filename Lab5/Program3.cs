using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class Program3
    {
        public void p3() 
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);  
            stack.Push(7);

            foreach (int i in stack)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine();

            Console.WriteLine("POP");
            stack.Pop();
            foreach (int i in stack)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine();
            Console.WriteLine(stack.Peek());

           Console.WriteLine();
           Console.WriteLine(stack.Contains(1));

           Console.WriteLine();
            stack.Clear();

            Console.WriteLine();
            Console.WriteLine(stack.Peek());
        }
    }
}
