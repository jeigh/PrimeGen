

using System.ComponentModel.DataAnnotations;

namespace PrimeGen.TransferObjects
{

    public class BigPrime
    {
        public BigPrime(System.Numerics.BigInteger primeValue)
        {
            PrimeValue = primeValue;
        }

        public BigPrime() {}


        [Key]
        public long BigPrimeId { get; set; }
        public System.Numerics.BigInteger PrimeValue { get; set; }
    }
}