using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_3841_4638
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome4638();
            Welcome3841();
            Console.ReadKey();
        }
        static partial void Welcome3841();
        private static void Welcome4638()
        {
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }

    }
}
