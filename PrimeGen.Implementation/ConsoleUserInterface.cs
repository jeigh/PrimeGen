using System;
using System.Numerics;
using System.Threading.Tasks;

namespace PrimeGen.Implementation
{
    public class ConsoleUserInterface
    {

        public int DashboardRefreshFrequencyInSeconds { get; set; } = 10;

        public void UpdateDashboard(long elapsedMilliseconds,  long primeCount, BigInteger potentialPrime, ref long previousSecondsElapsed)
        {
            long elapsedSeconds = elapsedMilliseconds / 1000;
            if (elapsedSeconds > previousSecondsElapsed + DashboardRefreshFrequencyInSeconds - 1)
            {
                long primesPerCount = elapsedSeconds == 0 ? primeCount : primeCount / elapsedSeconds;
                BigInteger intPerPrime = elapsedSeconds == 0 ? primeCount : BigInteger.Divide(potentialPrime, primeCount);
                Console.Out.WriteLineAsync($"Prime Found: {potentialPrime},\tTotal Primes Found: {primeCount},\tSeconds Elapsed: {elapsedSeconds},\tInts per primes: {intPerPrime}");
                previousSecondsElapsed = elapsedSeconds;
            }
            
        }
    }
}
