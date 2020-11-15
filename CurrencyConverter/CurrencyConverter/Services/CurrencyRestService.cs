using CurrencyConverter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyConverter.Services
{
    public static class CurrencyRestService
    {
        private static HttpClient httpClient = new HttpClient();

        public static LatestCurrency LatestCurrency { get; private set; }

        public static async Task<LatestCurrency> GetLatestCurrencyAsync()
        {
            LatestCurrency = new LatestCurrency();

            try
            {
                var response = await httpClient.GetAsync(new Uri("https://api.exchangeratesapi.io/latest"));
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    LatestCurrency = JsonConvert.DeserializeObject<LatestCurrency>(content);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return LatestCurrency;
        }
    }
}
