using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public decimal Rate { get; set; }
        public DateTime Date { get; set; }
    }
}