using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Program5
    {
        class ShoppingCart
        {
            string name;
            int quantity;
            double price;
            int itemCode;

            public void addItem(string name , int quantity)
            {
                this.name = name;
                this.quantity = quantity;
                this.price = 0;
                this.itemCode = 0;

            }

            public void addItem(string name, int quantity , double price)
            {
                this.name = name;
                this.quantity = quantity;
                this.price = price;
                this.itemCode = 0;
            }

            public void addItem(int itemCode, int quantity)
            {
                this.name = "";
                this.price = 0;
                this.itemCode = itemCode;
                this.quantity = quantity;
            }

            public void printCart()
            {
                Console.WriteLine($"Name : {name}");
                Console.WriteLine($"Quantity : {quantity}");
                Console.WriteLine($"Price : {price}");
                Console.WriteLine($"Item ID : {itemCode}");
            }

        }

          public void p5()
        {
            ShoppingCart s1 = new ShoppingCart();
            s1.addItem("milk",5);
            s1.printCart();
            Console.WriteLine();
            s1.addItem("Bread", 1, 50);
            s1.printCart();
            Console.WriteLine();
            s1.addItem(12, 5);
            s1.printCart();
            Console.WriteLine();
        }
    }
}
