using System.Collections.Generic;
using System.Numerics;

namespace PrimeGenCS
{
    public interface IDataAccess
    {
        void AddBigIntegerPrime(BigInteger prime);
        void AddLongPrime(long prime);

        void PersistBigIntegerPrimes(IEnumerable<BigInteger> bigIntegers);
        void PersistLongPrimes(IEnumerable<long> smallIntegers);

    }
}
