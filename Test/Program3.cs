using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program3
    {
        public class Book
        {
            int BookId;
            string Title;
            string Author;
            public Book(int b, string t ,string a)
            {
                this.BookId = b;
                this.Title = t;
                this.Author = a;

            }

            public void displayDetailsO()
            {
                Console.WriteLine($"ID  = {BookId}");
                Console.WriteLine($"Title = {Title}");
                Console.WriteLine($"Author = {Author}");
            }
        }

        public void p3()
        {
            Book b1 = new Book(1,"Hello World" , "Jenil");
            b1.displayDetailsO();
        }
    }
}
