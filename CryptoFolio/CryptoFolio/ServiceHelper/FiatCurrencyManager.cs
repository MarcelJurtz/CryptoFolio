using CryptoFolio.ServiceHelper.Values;
using System;
using System.Collections.Generic;

namespace CryptoFolio.ServiceHelper
{
    public class FiatCurrencyManager
    {
        public List<FiatCurrency> GetAllSupportedCurrencies()
        {
            List<FiatCurrency> currencies = new List<FiatCurrency>();

            currencies.Add(new FiatCurrency { ID = "USD", Name = "US Dollar", Symbol = "$", ApiConvertAppendix = "" });
            currencies.Add(new FiatCurrency { ID = "EUR", Name = "Euro", Symbol = "€", ApiConvertAppendix = "EUR" });

            return currencies;
        }

        public FiatCurrency GetFiatCurrencyById(String id)
        {
            return GetAllSupportedCurrencies().Find(x => x.ID == id);
        }

        public FiatCurrency GetDefaultFiatCurrency()
        {
            return GetAllSupportedCurrencies().Find(x => x.ID == PreferenceManager.DefaultCurrencyId);
        }
    }
}
