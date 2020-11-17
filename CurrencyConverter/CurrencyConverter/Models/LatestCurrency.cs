using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.Models
{
    // API Site: https://api.exchangeratesapi.io/latest
    public class LatestCurrency
    {
        public Rates Rates { get; set; } = new Rates();
        public string Base { get; set; }
        public string Date { get; set; }
    }
}
