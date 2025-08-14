using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{

     class BankAccount
    {
         decimal balance;
         string accountHolderName;

        public BankAccount(decimal initialBalance, string accountHolderName)
        {
            this.balance = initialBalance;
            this.accountHolderName = accountHolderName;
        }
        public void Deposit(decimal amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount} into your account. New balance: {balance}");
        }

        public void Deposit(string paymentMethod, decimal amount)
        {
            if (paymentMethod.ToLower() == "cash" || paymentMethod.ToLower() == "check")
            {
                balance += amount;
                Console.WriteLine($"Deposited {amount} using {paymentMethod} into your account. New balance: {balance}");
            }
            else
            {
                Console.WriteLine("Invalid payment method.");
            }
        }
        public void Withdraw(decimal amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew {amount} from your account. New balance: {balance}");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }
        public void Withdraw(string paymentMethod, decimal amount)
        {
            if (paymentMethod.ToLower() == "cash" || paymentMethod.ToLower() == "check")
            {
                if (balance >= amount)
                {
                    balance -= amount;
                    Console.WriteLine($"Withdrew {amount} using {paymentMethod} from your account. New balance: {balance}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
            }
            else
            {
                Console.WriteLine("Invalid payment method.");
            }
        }

        public void GetBalance()
        {
            Console.WriteLine(balance);
        }

        public void GetAccountHolderName()
        {
            Console.WriteLine(accountHolderName);
        }
    }

    public class Program6
    {
        public void p6()
        {
            BankAccount b1 = new BankAccount(1000000,"Jenil Patel");
            b1.Deposit(500);
            Console.WriteLine();
            b1.Withdraw(100);
            Console.WriteLine();
            b1.GetAccountHolderName();
            Console.WriteLine();
            b1.GetBalance();
            Console.WriteLine();
        }
    }
}
