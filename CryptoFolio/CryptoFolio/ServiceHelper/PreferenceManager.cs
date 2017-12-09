using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace CryptoFolio.ServiceHelper
{
    class PreferenceManager
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        private const string CURRENCY_KEY = "default_currency";
        private const string CURRENCY_DEFAULT = "USD";
        public static String DefaultCurrencyId
        {
            get
            {
                return AppSettings.GetValueOrDefault(CURRENCY_KEY, CURRENCY_DEFAULT);
            }
            set
            {
                AppSettings.AddOrUpdateValue(CURRENCY_KEY, value);
            }
        }
    }
}
