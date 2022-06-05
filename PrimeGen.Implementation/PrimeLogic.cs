using PrimeGen.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;

namespace PrimeGen.Implementation
{
    public class PrimeLogic
    {
        private readonly IDataAccess _dataAccess = new MockDataAccess();
        private readonly ConsoleUserInterface _ui;
        public BigInteger MaxPrime { get; set; } = -1;

        public PrimeLogic(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _ui = new ConsoleUserInterface();
        }

        public void GeneratePrimes(Stopwatch sw, long previousSecondsElapsed)
        {
            bool genFromScratch = !_dataAccess.GetSmallPrimes().Any();

            SynchronizedCollection<BigInteger> bigPrimes = new SynchronizedCollection<BigInteger>();
            SynchronizedCollection<long> smallPrimes = new SynchronizedCollection<long>();

            BigInteger potentialPrime = 1;

            if (genFromScratch)
            {
                long knownFirstPrime = 2;
                _dataAccess.AddSmallPrime(new TransferObjects.SmallPrime(knownFirstPrime));  
                smallPrimes.Add(knownFirstPrime);
            }
            else
            {
                //todo: convert bigprime to synchronizedCollection like we did with smallprimes
                var bigPrimesTemp = (from prime in _dataAccess.GetBigPrimes() select prime.PrimeValue).ToList();
                foreach(BigInteger prime in bigPrimesTemp)
                {
                    bigPrimes.Add(prime);
                }


                List<long> tempSmallPrimes = (from prime in _dataAccess.GetSmallPrimes() select prime.PrimeValue).ToList();
                foreach(long smallPrime in tempSmallPrimes)
                {
                    smallPrimes.Add(smallPrime);
                }

                if (smallPrimes.Any()) potentialPrime = smallPrimes.Max();
                if (bigPrimes.Any()) potentialPrime = bigPrimes.Max();
            }

            RunPrimeGenerationLoop(potentialPrime, smallPrimes, bigPrimes, sw, ref previousSecondsElapsed);
        }

        private void RunPrimeGenerationLoop(BigInteger potentialPrime, SynchronizedCollection<long> smallPrimes, SynchronizedCollection<BigInteger> bigPrimes, Stopwatch sw, ref long previousSecondsElapsed)
        {
            var waitable = new List<Task>();
            while (this.MaxPrime == -1 || potentialPrime + 1 <= this.MaxPrime || potentialPrime >= potentialPrime + 1000)
            {
                potentialPrime += 2;

                if (long.TryParse(potentialPrime.ToString(), out long potentialPrimeLong))
                {
                    Task myTask = new Task(delegate { this.HandleSmallPrime(smallPrimes, potentialPrimeLong, sw); }); 
                    myTask.Start();
                    waitable.Add(myTask);
                }

                
                
                    
                else  // todo: use threadpools?
                    this.HandleBigIntegerPrime(smallPrimes, bigPrimes, potentialPrime, sw, ref previousSecondsElapsed);
            }

            Task.WaitAll(waitable.ToArray());

        }


        public void HandleBigIntegerPrime(SynchronizedCollection<long> smallPrimes, SynchronizedCollection<BigInteger> bigPrimes, BigInteger potentialPrime, Stopwatch sw, ref long previousSecondsElapsed)
        {
            BigInteger squareRoot = GetSquareRoot(potentialPrime);

            bool isPrime = IsThisOnePrime(smallPrimes, bigPrimes, potentialPrime, squareRoot);

            if (isPrime)
            {
                _dataAccess.AddBigIntegerPrime(new TransferObjects.BigPrime(potentialPrime));
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

        public void HandleSmallPrime(SynchronizedCollection<long> smallPrimes, long potentialPrimeLong, Stopwatch sw)
        {
            long squareRoot = GetSquareRoot(potentialPrimeLong);
            bool isPrime = IsThisOnePrime(smallPrimes, new List<BigInteger>(), potentialPrimeLong, squareRoot);

            if (isPrime)
            {
                _dataAccess.AddSmallPrime(new TransferObjects.SmallPrime(potentialPrimeLong));
                smallPrimes.Add(potentialPrimeLong);

                long primeCount = smallPrimes.Count();

                if (sw != null)
                    _ui.UpdateDashboard(sw.ElapsedMilliseconds, primeCount, potentialPrimeLong);
            }
        }


        public void HandleSmallPrime(SynchronizedCollection<long> smallPrimes, long potentialPrimeLong, Stopwatch sw, ref long previousSecondsElapsed)
        {
            long squareRoot = GetSquareRoot(potentialPrimeLong);

            bool isPrime = IsThisOnePrime(smallPrimes, new List<BigInteger>(), potentialPrimeLong, squareRoot);

            if (isPrime)
            {
                _dataAccess.AddSmallPrime(new TransferObjects.SmallPrime(potentialPrimeLong));
                smallPrimes.Add(potentialPrimeLong);

                long primeCount = smallPrimes.Count();

                if (sw != null)
                    _ui.UpdateDashboard(sw.ElapsedMilliseconds, primeCount, potentialPrimeLong, ref previousSecondsElapsed);
            }
        }

        public bool IsThisOnePrime(SynchronizedCollection<long> smallPrimes, IList<BigInteger> bigPrimes, BigInteger potentialPrime, BigInteger squareRoot)
        {
            foreach (long prime in smallPrimes)
            {
                if (prime > squareRoot) return true;
                if (potentialPrime % prime == 0) return false;
            }
            foreach(BigInteger prime in bigPrimes)
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
