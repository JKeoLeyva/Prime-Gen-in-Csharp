using System;
using System.Diagnostics;

/**
 * Author: Jorge Leyva
 * Program that will generate large prime numbers using the C# parallel Libraries
 */
namespace PrimeGen
{
    class Program
    {
        /*
         * The usageMessage method displays a help message on how to properly use the program whenever incorrect
         * usage of the program is done
         */
        public static void UsageMessage()
        {
            Console.WriteLine("dotnet run PrimeGen <bits> <count=1>");
            Console.WriteLine("    - bits - the number of bits of the prime number, this must be a multiple of 8, " +
                              "and at least 32 bits");
            Console.WriteLine("    - count - the number of prime numbers to generates, defaults to 1");
            Environment.Exit(0);
        }
        
        static void Main(string[] args)
        {
            int bits = 0;
            int count = 1;
            if (args.Length == 1)
            {
                try
                {
                    bits = int.Parse(args[0]);
                }
                catch (Exception e)
                {
                    UsageMessage();
                    throw e;
                }
                if (bits < 32 || bits % 8 != 0)
                {
                    UsageMessage();
                }
            }
            if (args.Length == 2)
            {
                try
                {
                    bits = int.Parse(args[0]);
                    count = int.Parse(args[1]);
                }
                catch (Exception e)
                {
                    UsageMessage();
                    throw e;
                }
                if (bits < 32 || bits % 8 != 0)
                {
                    UsageMessage();
                }
            }

            if (args.Length > 2 || args.Length < 1)
            {
                UsageMessage();
            }
            
            var png = new PrimeNumGen(bits, count);
            Stopwatch stopwatch = new Stopwatch();
            
            Console.WriteLine("dotnet run {0} {1}", bits, count);
            Console.WriteLine("BitLength: {0}", bits);

            stopwatch.Start();
            png.run();
            stopwatch.Stop();

            Console.WriteLine("Time to Generate: {0}", stopwatch.Elapsed);
        }
    }
}