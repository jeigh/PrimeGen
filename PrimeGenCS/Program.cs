using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeGenCS
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ulong> primes = new List<ulong>();
            
            ulong iterator = 2;
            
            primes.Add(iterator);
            Console.WriteLine(iterator);

            Stopwatch sw = new Stopwatch();

            sw.Start();
            while (true)
            {
                iterator += 1;

                //todo: it might be more efficient to calculate the square root differently...  Possibly keep calculating square roots until the next integral square root is reached.
                double squareRoot = Math.Sqrt(iterator);
                bool isPrime = true;

                //todo: instead of using primes here, use a truncated list of primes that's only expanded to a max value of the subject integer's square root.
                foreach (ulong prime in primes)
                {
                    if (prime > squareRoot)
                        break;

                    if (iterator % prime == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    //todo: log the number into a database
                    primes.Add(iterator);
                    long primeCount = primes.LongCount();
                    long elapsedSeconds = sw.ElapsedMilliseconds / 1000;
                    long primesPerCount = elapsedSeconds == 0 ? primeCount : primeCount / elapsedSeconds;
                    Console.WriteLine($"Prime Found: {iterator},\tTotal Primes Found: {primeCount},\tSeconds Elapsed: {elapsedSeconds},\tPrimes per Second: {primesPerCount}");
                }
            }
            sw.Stop();

        }
    }
}
