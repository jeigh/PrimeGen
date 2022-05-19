using PrimeGen.TransferObjects;
using System.Collections.Generic;
using System.Numerics;

namespace PrimeGen.DataAccess
{
    public interface IDataAccess
    {
        void AddBigIntegerPrime(BigPrime prime);
        void AddSmallPrime(SmallPrime prime);
        IEnumerable<BigPrime> GetBigPrimes();
        IEnumerable<SmallPrime> GetSmallPrimes();
    }
}
