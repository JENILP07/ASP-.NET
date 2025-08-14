using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal class Staff
    {
        public string name;
        public string department;
        public string designation;
        public int experience;
        public int salary;

        public void getDetails()
        {
            Console.Write("Enter Name : ");
            name = Console.ReadLine();

            Console.Write("Enter Department : ");
            department = Console.ReadLine();

            Console.Write("Enter Designation : ");
            designation = Console.ReadLine();

            Console.Write("Enter Experience(Years) : ");
            experience = int.Parse(Console.ReadLine());

            Console.Write("Enter Salary(in Rs.) : ");
            salary = int.Parse(Console.ReadLine());
        }
        public void displayDetails()
        {

            Console.WriteLine("Name : " + name);
            Console.WriteLine("Salary : " + "Rs." + salary );
        }
    }
}
