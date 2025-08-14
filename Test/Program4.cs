using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program4
    {
        class Employee
        {
            int employeeId;
            string name;
            double salary;
            double annualSalary;

            public void inputDetails(int id, string name, double salary)
            {
                this.employeeId = id;
                this.name = name;
                this.salary = salary;

            }

            public void displayDetails()
            {
                Console.WriteLine($"Id  = {employeeId}");
                Console.WriteLine($"Name  = {name}");
                Console.WriteLine($"Salary = {salary}");
                Console.WriteLine($"Annual Salary = {annualSalary}");
            }

            public void calculateAnnualSalary()
            {
                annualSalary = salary * 12;
            }

            public void determinePerformance()
            {
                if (annualSalary > 100000) { Console.WriteLine("Outstanding"); }
                else if (annualSalary <= 10000000 || annualSalary >= 500000) { Console.WriteLine("Excellent"); }
                else if (annualSalary < 500000 || annualSalary >= 250000) { Console.WriteLine("Good"); }
                else { Console.WriteLine("Needs Improvement"); }


            }
        }
        public void p4()
        {
            Employee emp = new Employee();
            emp.inputDetails(101, "Jenil", 100000);
            emp.calculateAnnualSalary();
            emp.displayDetails();
            Console.WriteLine();
            emp.determinePerformance();
            Console.WriteLine();
        }

    }
}
