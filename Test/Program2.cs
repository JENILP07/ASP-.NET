using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program2
    {

        public void p2()
        {
            Console.Write("Enter Seconds : ");
            int sec = Convert.ToInt32(Console.ReadLine());
            ConvertSecondsToHMS(sec);
        }
        public static void ConvertSecondsToHMS(int seconds)
        {
            int hours = seconds / 3600;
            int minutes = (seconds % 3600) / 60;
            int secondsRemaining = seconds % 60;

            Console.WriteLine($"{hours}:{minutes}:{secondsRemaining}");
        }
    }
}
