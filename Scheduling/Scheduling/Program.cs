using System;
using Serilog;

namespace Scheduling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Console.WriteLine("Hello World!");
        }
    }
}
