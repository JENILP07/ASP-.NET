using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal class Student
    {
        public long Enrollment_No;
        public string Student_Name;
        public int Semester;
        public float CPI;
        public float SPI;

        public Student()
        {
            Console.Write("Enter Enrollment_No : ");
            Enrollment_No = long.Parse(Console.ReadLine());

            Console.Write("Enter Student_Name : ");
            Student_Name = Console.ReadLine();

            Console.Write("Enter Semester : ");
            Semester = int.Parse(Console.ReadLine());

            Console.Write("Enter SPI : ");
            SPI = float.Parse(Console.ReadLine());

            Console.Write("Enter CPI : ");
            CPI = float.Parse(Console.ReadLine());
        }
        public void displayStudentDetails()
        {
            Console.WriteLine("\n\nStudent Details\n");

            Console.WriteLine("Enrollment_No : "+Enrollment_No);
            Console.WriteLine("Student_Name : " + Student_Name);
            Console.WriteLine("Semester : "+Semester);
            Console.WriteLine("SPI : "+SPI);
            Console.WriteLine("CPI : "+CPI);
        }
    }
}
