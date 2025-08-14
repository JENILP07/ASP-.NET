using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal class Bank_Account
    {
        public int account_No;
        public string email;
        public string user_Name;
        public string account_Type;
        public int account_Balance;

        public void getAccountDetails()
        {
            Console.Write("Enter Account_No : ");
            account_No = int.Parse(Console.ReadLine());

            Console.Write("Enter Email : ");
            email = Console.ReadLine();

            Console.Write("Enter User_Name : ");
            user_Name = Console.ReadLine();

            Console.Write("Enter Account_Type : ");
            account_Type = Console.ReadLine();

            Console.Write("Enter Account_Balance : ");
            account_Balance = int.Parse(Console.ReadLine());
        }
        public void displayAccountDetails()
        {
            Console.WriteLine("\n\nAccount Details\n");

            Console.WriteLine("Account_No : " + account_No);
            Console.WriteLine("Email : "+ email);
            Console.WriteLine("Account_Type : "+account_Type);
            Console.WriteLine("Account_Balance : Rs. "+account_Balance);
        }
    }
}
