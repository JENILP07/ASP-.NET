using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Program3
    {
        class RBI
        {
            public virtual void calculateIntrest(int p, int r, int t)
            {
                Console.WriteLine($"Intrest is : {(p * r * t)/100}");
            }
        }
        class HDFC : RBI 
        {
            public override void calculateIntrest(int p, int r, int t)
            {
                base.calculateIntrest(p, r, t);
            }
        }

        class SBI : RBI
        {
            public override void calculateIntrest(int p, int r, int t)
            {
                base.calculateIntrest(p, r, t);
            }
        }

        class ICICI : RBI
        {
            public override void calculateIntrest(int p, int r, int t)
            {
                base.calculateIntrest(p, r, t);
            }
        }

        public void p3() 
        {
            Console.WriteLine("HDFC");
            HDFC a1 = new HDFC();
            a1.calculateIntrest(1000, 2, 1);

            Console.WriteLine("SBI");
            SBI a2 = new SBI(); 
            a2.calculateIntrest(1000, 3, 1);

            Console.WriteLine("ICICI");
            ICICI a3 = new ICICI(); 
            a3.calculateIntrest(1000, 1, 1);
        }
    }
}
