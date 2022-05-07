using System;
using System.IO;

namespace DataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName);
        }

    }
}
