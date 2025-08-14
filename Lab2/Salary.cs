using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal class Salary
    {
        public double Basic;
        public double TA;
        public double DA;
        public double HRA;

        public Salary(double basic, double tA, double dA = 10, double hRA = 20)
        {
            Basic = basic;
            TA = tA;
            DA = dA;
            HRA = hRA;
        }
        public double CalculateSalary()
        {
            double totalDA = (Basic * DA) / 100;
            double totalHRA = (Basic * HRA) / 100;
            double totalSalary = Basic + TA + totalDA + totalHRA;
            return totalSalary;
        }

        public void DisplaySalaryDetails()
        {
            Console.WriteLine("Basic Salary: " + Basic);
            Console.WriteLine("Travel Allowance (TA): " + TA);
            Console.WriteLine("Dearness Allowance (DA): " + DA + "%");
            Console.WriteLine("House Rent Allowance (HRA): " + HRA + "%");
            Console.WriteLine("Total Salary: " + CalculateSalary());
        }
    }
}
