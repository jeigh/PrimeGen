using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace PrimeGenCS
{
    public class PrimeLogic
    {
        private readonly IDataAccess _dataAccess = new MockDataAccess();
        private readonly ConsoleUserInterface _ui = new ConsoleUserInterface();
        public BigInteger MaxPrime { get; set; } = -1;
        

        public PrimeLogic(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public void GeneratePrimes(Stopwatch sw, long previousSecondsElapsed)
        {
            bool genFromScratch = true;  // todo:  does existing workload exist?

            List<BigInteger> bigPrimes = new List<BigInteger>();
            List<long> smallPrimes = new List<long>();

            BigInteger potentialPrime = 1;

            if (genFromScratch)
            {
                _dataAccess.AddLongPrime(2);  // known first prime
            }
            else
            {
                //todo:  Future revision should pull previous workload from a database instead of starting from scratch everytime                   
                // potentialPrime = ?
                // bigPrimes = ?
                // smallPrimes = ?
            }

            while (this.MaxPrime == -1 || potentialPrime + 1 <= this.MaxPrime)
            {
                potentialPrime += 1;

                if (long.TryParse(potentialPrime.ToString(), out long potentialPrimeLong))
                    this.HandleLongPrime(smallPrimes, potentialPrimeLong, sw, ref previousSecondsElapsed);
                else
                    this.HandleBigIntegerPrime(smallPrimes, bigPrimes, potentialPrime, sw, ref previousSecondsElapsed);
            }
        }


        public void HandleBigIntegerPrime(IEnumerable<long> smallPrimes, IList<BigInteger> bigPrimes, BigInteger potentialPrime, Stopwatch sw, ref long previousSecondsElapsed)
        {
            BigInteger squareRoot = GetSquareRoot(potentialPrime);

            bool isPrime = IsThisOnePrime(smallPrimes, bigPrimes, potentialPrime, squareRoot);

            if (isPrime)
            {
                _dataAccess.AddBigIntegerPrime(potentialPrime);
                bigPrimes.Add(potentialPrime);

                long primeCount;
                try
                {
                    primeCount = smallPrimes.Count() + bigPrimes.Count();
                }
                catch (OverflowException ex)
                {
                    primeCount = long.MaxValue;
                }
                
                if (sw != null)
                    _ui.UpdateDashboard(sw.ElapsedMilliseconds, primeCount, potentialPrime, ref previousSecondsElapsed);

            }

        }

        public void HandleLongPrime(IList<long> smallPrimes, long potentialPrimeLong, Stopwatch sw, ref long previousSecondsElapsed)
        {
            long squareRoot = GetSquareRoot(potentialPrimeLong);

            bool isPrime = IsThisOnePrime(smallPrimes, new List<BigInteger>(), potentialPrimeLong, squareRoot);

            if (isPrime)
            {
                _dataAccess.AddLongPrime(potentialPrimeLong);
                smallPrimes.Add(potentialPrimeLong);

                long primeCount = smallPrimes.Count();

                if (sw != null)
                    _ui.UpdateDashboard(sw.ElapsedMilliseconds, primeCount, potentialPrimeLong, ref previousSecondsElapsed);
            }
        }

        public bool IsThisOnePrime(IEnumerable<long> smallPrimes, IEnumerable<BigInteger> bigPrimes, BigInteger potentialPrime, BigInteger squareRoot)
        {
            foreach (long prime in smallPrimes.ToList())
            {
                if (prime > squareRoot) return true;
                if (potentialPrime % prime == 0) return false;
            }
            foreach(BigInteger prime in bigPrimes.ToList())
            {
                if (prime > squareRoot) return true;
                if (potentialPrime % prime == 0) return false;
            }
            return true;
        }


        public long GetSquareRoot(long potentialPrime)
        {
            double unrounded = Math.Sqrt(potentialPrime);
            return (long)Math.Ceiling(unrounded);
        }

        public BigInteger GetSquareRoot(BigInteger potentialPrime)
        {
            //todo: sqrt for biginteger doesn't exist...
            
            //can make this much faster by calculating the real square root using bespoke calculation...
            // also something on google...
            
            // as - is, this next line makes it go really slow once you hit BigInteger territory because
            // the output of this method serves to inform us the upper bounds of what the greatest factor might be

            return potentialPrime;
        }

    }
}
