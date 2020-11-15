using CurrencyConverter.Models;
using CurrencyConverter.Services;
using CurrencyConverter.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.ViewModels
{
    public class ConverterViewModel : BaseViewModel
    {
        private LatestCurrency _latestCurrency;
        public LatestCurrency LatestCurrency 
        { 
            get { return _latestCurrency; }
            set { SetValue(ref _latestCurrency, value); }
        }

        public ConverterViewModel()
        {
            GetLatestCurrency();
        }

        private async void GetLatestCurrency()
        {
            //LatestCurrency = await CurrencyRestService.GetLatestCurrencyAsync();
            LatestCurrency = await GlobalStorage.GetLatestCurrencyAsync();
        }
    }
}
