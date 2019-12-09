using System;
using ACO.Attempt2;

namespace ACO
{
    class Program
    {
        
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            Algorithm algorithm = new Algorithm();
            algorithm.Execute(10, 2.5f, 0.1f, 0.9f);

        }
    }
}