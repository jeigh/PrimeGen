

using System.ComponentModel.DataAnnotations;

namespace PrimeGen.TransferObjects
{
    public class SmallPrime
    {
        public SmallPrime(long primeValue)
        {
            PrimeValue = primeValue;
        }

        public SmallPrime()
        {
        }

        [Key]
        public long SmallPrimeId { get; set; }
        public long PrimeValue { get; set; }
    }
}