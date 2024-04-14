using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExchangeRates
{
    internal class ExchangeRateContext : DbContext
    {
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public ExchangeRateContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=exchangeRates.db");
        }
    }
}