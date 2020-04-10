using System;
using filecopier;

namespace runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var copier = new Copier();
            copier.Process("/Users/louise/Documents/Photos/20200403/export");
            Console.WriteLine("Hello World!");
        }
    }
}
