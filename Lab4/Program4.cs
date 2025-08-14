using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Program4
    {
        class Hospital
        {
            public virtual void hospitalDetails()
            {
                Console.WriteLine("This is a Hospital");
            }
        }

        class Apollo : Hospital
        {
            public override void hospitalDetails()
            {
                base.hospitalDetails();
                Console.WriteLine("This is Apollo");
            }
        }

        class Wockhardt : Hospital
        {
            public override void hospitalDetails()
            {
                base.hospitalDetails();
                Console.WriteLine("This is Wockhardt");
            }
        }

        class Gokul_Superspeciality : Hospital
        {
            public override void hospitalDetails()
            {
                base.hospitalDetails();
                Console.WriteLine("This is Gokul Superspeciality");
            }
        }

        public void p4()
        {
            Apollo x1 = new Apollo();
            x1.hospitalDetails();
            Console.WriteLine();

            Wockhardt x2 = new Wockhardt();
            x2.hospitalDetails();
            Console.WriteLine();

            Gokul_Superspeciality x3 = new Gokul_Superspeciality();
            x3.hospitalDetails();
            Console.WriteLine();
        }
    }
}
