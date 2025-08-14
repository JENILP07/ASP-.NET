using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extra1
{
    internal class Program7
    {
        public void P7()
        {
            Time t1 = new Time(10 , 50 ,59);
            Console.WriteLine();
            Time t2 = new Time(5, 10, 15);
            Console.WriteLine();
            Time tcopy = new Time(t1);
            Console.WriteLine();
            t1.addTime(t2);
            Console.WriteLine();
        }

        class Time
        {
            int hours;
            int minutes;
            int secondsRemaining;

            public Time(int hours, int minutes, int secondsRemaining)
            {
                this.hours = hours;
                this.minutes = minutes;
                this.secondsRemaining = secondsRemaining;
                printTime();

            }

            public Time(Time previousTime) { 
            
                hours = previousTime.hours;
                minutes = previousTime.minutes;
                secondsRemaining = previousTime.secondsRemaining;
                Console.Write("Copy Constructor : ");
                printTime();

            }   

            public void printTime()
            {
                Console.WriteLine($"Hour:{hours} Min:{minutes} Sec:{secondsRemaining}");
            }
            
            public void addTime(Time x)
            {
                this.hours += x.hours;
                this.minutes += x.minutes; 
                this.secondsRemaining += x.secondsRemaining;
                if (this.minutes >= 60) {
                    this.minutes -= 60;
                    this.hours += 1;
                }
                
                
                if (this.secondsRemaining >= 60)
                {
                    this.secondsRemaining -= 60;
                    this.minutes += 1;
                }
             
                printTime();
            }
        }
    }
}
