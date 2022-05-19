using PrimeGen.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeGen.DataAccess
{

    public class EntityFrameworkDataAccess : IDataAccess
    {
        public readonly PrimeGenContext _context;

        public EntityFrameworkDataAccess()
        {
            var dbContextOptons = new Microsoft.EntityFrameworkCore.DbContextOptions<PrimeGenContext>();
            
            
            _context = new PrimeGenContext(dbContextOptons);
            //todo: put this in a config somewhere
            _context.ConnectionString = @"Server=(localdb)\\mssqllocaldb;Database=PrimeGen;Trusted_Connection=True;MultipleActiveResultSets=true";

            //_context.Database.EnsureCreated();
        }

        public void AddBigIntegerPrime(BigPrime prime)
        {
            _context.Add(prime);
            _context.SaveChanges();
        }

        public void AddSmallPrime(SmallPrime prime)
        {
            _context.Add(prime);
            _context.SaveChanges();
        }

        public IEnumerable<BigPrime> GetBigPrimes()
        {
            return _context.BigPrimes.ToList();
        }

        public IEnumerable<SmallPrime> GetSmallPrimes()
        {
            return _context.SmallPrimes.ToList();
        }
    }
}
