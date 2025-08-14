using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal class Account_Details
    {
        public string AccountHolderName;
        public double AccountBalance;
        public double AnnualInterestRate;

        public void GetAccountDetails()
        {
            Console.Write("Enter Account Holder's Name: ");
            AccountHolderName = Console.ReadLine();

            Console.Write("Enter Account Balance: ");
            AccountBalance = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Annual Interest Rate (in %): ");
            AnnualInterestRate = Convert.ToDouble(Console.ReadLine());
        }
    }
    internal class Interest : Account_Details
    {
        public void CalculateAndDisplayInterest()
        {
            double interest = (AccountBalance * AnnualInterestRate) / 100;

            Console.WriteLine("\n--- Account Details and Interest ---");
            Console.WriteLine($"Account Holder: {AccountHolderName}");
            Console.WriteLine($"Account Balance: {AccountBalance:C}");
            Console.WriteLine($"Annual Interest Rate: {AnnualInterestRate}%");
            Console.WriteLine($"Total Interest for the year: {interest:C}");
        }
    }
}
