using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal class Candidate
    {
        public int ID;
        public string name;
        public int age;
        public float weight;
        public float height;

        public void GetCandidateDetails()
        {
            Console.Write("Enter ID : ");
            ID = int.Parse(Console.ReadLine());

            Console.Write("Enter Name : ");
            name = Console.ReadLine();

            Console.Write("Enter Age : ");
            age = int.Parse(Console.ReadLine());

            Console.Write("Enter Weight(Kg) : ");
            weight = float.Parse(Console.ReadLine());

            Console.Write("Enter Height(cm) : ");
            height = float.Parse(Console.ReadLine());
        }
        public void DisplayCandidateDetails()
        {
            Console.WriteLine("\n\nCandidate Details -> \n");

            Console.WriteLine("Candidate ID : "+ID);
            Console.WriteLine("Candidate Name : "+ name);
            Console.WriteLine("Candidate Age : "+ age);
            Console.WriteLine("Candidate Weight : " + weight+" Kg");
            Console.WriteLine("Candidate Height : " + height+" cm");
        }
    }
}
