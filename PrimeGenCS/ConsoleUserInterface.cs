using System;
using System.Numerics;
using System.Threading.Tasks;

namespace PrimeGenCS
{
    public class ConsoleUserInterface
    {
        
        public void UpdateDashboard(long elapsedMilliseconds,  long primeCount, BigInteger potentialPrime, ref long previousSecondsElapsed)
        {
            long elapsedSeconds = elapsedMilliseconds / 1000;
            if (elapsedSeconds > previousSecondsElapsed + 9)
            {
                long primesPerCount = elapsedSeconds == 0 ? primeCount : primeCount / elapsedSeconds;
                BigInteger intPerPrime = elapsedSeconds == 0 ? primeCount : BigInteger.Divide(potentialPrime, primeCount);
                Console.Out.WriteLineAsync($"Prime Found: {potentialPrime},\tTotal Primes Found: {primeCount},\tSeconds Elapsed: {elapsedSeconds},\tInts per primes: {intPerPrime}");
                previousSecondsElapsed = elapsedSeconds;
            }
            
        }
    }
}
