using PrimeGen.TransferObjects;
using System.Collections.Generic;
using System.Numerics;

namespace PrimeGen.DataAccess
{
    public class MockDataAccess : IDataAccess
    {
        public void AddBigIntegerPrime(BigPrime prime) =>
            BigPrimes.Add(prime);

        public void AddSmallPrime(SmallPrime prime) => 
            SmallPrimes.Add(prime);

        public List<SmallPrime> SmallPrimes { get; set; } = new List<SmallPrime>();
        public List<BigPrime> BigPrimes { get; set; } = new List<BigPrime>();

        public IEnumerable<BigPrime> GetBigPrimes()
        {
            throw new System.NotImplementedException("to be implemented with database feature");
        }

        public IEnumerable<SmallPrime> GetSmallPrimes()
        {
            throw new System.NotImplementedException("to be implemented with database feature");
        }
    }
}
