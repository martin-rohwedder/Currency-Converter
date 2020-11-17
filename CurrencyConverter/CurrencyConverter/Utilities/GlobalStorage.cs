using CurrencyConverter.Models;
using CurrencyConverter.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CurrencyConverter.Utilities
{
    public static class GlobalStorage
    {
        public static async Task<LatestCurrency> GetLatestCurrencyAsync()
        {
            LatestCurrency latestCurrency = null;

            if (Application.Current.Properties.ContainsKey("LatestDate"))
            {
                var latestDateString = Application.Current.Properties["LatestDate"] as string;
                DateTime latestDate = DateTime.Parse(latestDateString);

                switch (DateTime.Now.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                        latestCurrency = await FetchLatestCurrencyAsync(latestDate, true);
                        break;
                    case DayOfWeek.Sunday:
                        latestCurrency = await FetchLatestCurrencyAsync(latestDate, true);
                        break;
                    default:
                        latestCurrency = await FetchLatestCurrencyAsync(latestDate, false);
                        break;
                }
            }
            else
            {
                latestCurrency = await CurrencyRestService.GetLatestCurrencyAsync();
                Application.Current.Properties.Add("LatestDate", latestCurrency.Date);
                Application.Current.Properties.Add("LatestCurrencyJsonString", JsonConvert.SerializeObject(latestCurrency));
                await Application.Current.SavePropertiesAsync();
            }

            return latestCurrency;
        }

        private static async Task<LatestCurrency> FetchLatestCurrencyAsync(DateTime latestDate, bool isWeekend)
        {
            LatestCurrency latestCurrency = null;

            if (CompareLatestDateWithToday(latestDate))
            {
                var latestCurrencyJsonString = Application.Current.Properties["LatestCurrencyJsonString"] as string;
                latestCurrency = JsonConvert.DeserializeObject<LatestCurrency>(latestCurrencyJsonString);
            }
            else
            {
                if (isWeekend && latestDate.DayOfWeek == DayOfWeek.Friday)
                {
                    var latestCurrencyJsonString = Application.Current.Properties["LatestCurrencyJsonString"] as string;
                    latestCurrency = JsonConvert.DeserializeObject<LatestCurrency>(latestCurrencyJsonString);
                }
                else
                {
                    latestCurrency = await CurrencyRestService.GetLatestCurrencyAsync();
                    Application.Current.Properties["LatestDate"] = latestCurrency.Date;
                    Application.Current.Properties["LatestCurrenctJsonString"] = JsonConvert.SerializeObject(latestCurrency);
                    await Application.Current.SavePropertiesAsync();
                }
            }

            return latestCurrency;
        }

        /// <summary>
        /// Compare the latest date time date, with today (DateTime.Now).
        /// This comparison excluded the time.
        /// </summary>
        /// <param name="latestDateTime">The latest date time object</param>
        /// <returns>True, if the latest DateTime objects date is equal to today</returns>
        private static bool CompareLatestDateWithToday(DateTime latestDateTime)
        {
            return (DateTime.Now.Day == latestDateTime.Day && DateTime.Now.Month == latestDateTime.Month && DateTime.Now.Year == latestDateTime.Year);
        }
    }
}
