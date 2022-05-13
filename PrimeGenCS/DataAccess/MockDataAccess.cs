using System.Collections.Generic;
using System.Numerics;

namespace PrimeGenCS
{
    public class MockDataAccess : IDataAccess
    {
        public void AddBigIntegerPrime(BigInteger prime) =>
            BigIntegerPrimes.Add(prime);

        public void AddLongPrime(long prime) => 
            LongPrimes.Add(prime);

        public List<long> LongPrimes { get; set; } = new List<long>();
        public List<BigInteger> BigIntegerPrimes { get; set; } = new List<BigInteger>();


        public void PersistBigIntegerPrimes(IEnumerable<BigInteger> bigIntegers)
        {
            throw new System.NotImplementedException("to be implemented in next version");
        }

        public void PersistLongPrimes(IEnumerable<long> smallIntegers)
        {
            throw new System.NotImplementedException("to be implemented in next version");
        }

    }
}
