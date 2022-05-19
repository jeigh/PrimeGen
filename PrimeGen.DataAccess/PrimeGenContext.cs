using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrimeGen.TransferObjects;

namespace PrimeGen.DataAccess
{
    public class PrimeGenContext : DbContext
    {
        public string ConnectionString { get; set; } = string.Empty;
        public PrimeGenContext (DbContextOptions<PrimeGenContext> options) : base(options) 
        {

        }

        public DbSet<SmallPrime>? SmallPrimes { get; set; }
        public DbSet<BigPrime>? BigPrimes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString ?? throw new InvalidOperationException("Connection string 'PrimeGenContext' not found."));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SmallPrime>().ToTable("SmallPrime");
            modelBuilder.Entity<BigPrime>().ToTable("BigPrime");
        }   
    }
}
