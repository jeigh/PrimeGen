using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;

namespace PrimeGenCS
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BigInteger> primes = new List<BigInteger>();
            
            BigInteger iterator = 2;
            
            primes.Add(iterator);
            Console.WriteLine(iterator);

            Stopwatch sw = new Stopwatch();
            long previousSecondsElapsed = 0;
            sw.Start();
            while (true)
            {
                iterator += 1;

                //todo: it might be more efficient to calculate the square root differently...  Possibly keep calculating square roots until the next integral square root is reached.
//                var squareRoot = GetSquareRoot(iterator); 

                bool isPrime = true;

                //todo: instead of using primes here, use a truncated list of primes that's only expanded to a max value of the subject integer's square root.
                foreach (BigInteger prime in primes)
                {
                    //if (prime > squareRoot)
                    //    break;

                        if (iterator % prime == 0)
                    {
                        isPrime = false;
                        break;
                    }

                    if (prime * prime > iterator) break;
                }

                if (isPrime)
                {
                    //todo: log the number into a database
                    primes.Add(iterator);
                    long primeCount = primes.LongCount();
                    long elapsedSeconds = sw.ElapsedMilliseconds / 1000;
                    long primesPerCount = elapsedSeconds == 0 ? primeCount : primeCount / elapsedSeconds;
                    BigInteger intPerPrime = elapsedSeconds == 0 ? primeCount : BigInteger.Divide(iterator, primeCount);
                    if (elapsedSeconds > previousSecondsElapsed)
                    {
                        Console.WriteLine($"Prime Found: {iterator},\tTotal Primes Found: {primeCount},\tSeconds Elapsed: {elapsedSeconds},\tInts per primes: {intPerPrime}");
                    }
                    previousSecondsElapsed = elapsedSeconds;
                }
            }
            sw.Stop();

        }

        private static BigInteger GetSquareRoot(BigInteger iterator)
        {
            //todo: sqrt for biginteger doesn't exist...  can make this much faster by calculating the real square root using bespoke calculation...

            //if (iterator > long.MaxValue) 
                return iterator;


        }
    }
}
