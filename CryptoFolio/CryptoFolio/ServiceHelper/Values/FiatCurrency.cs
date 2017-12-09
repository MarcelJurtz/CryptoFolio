﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoFolio.ServiceHelper.Values
{
    public class FiatCurrency
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public String Symbol { get; set; }
        public String ApiParameter { get; set; }

        public static List<FiatCurrency> GetAllSupportedCurrencies()
        {
            List<FiatCurrency> currencies = new List<FiatCurrency>();

            currencies.Add(new FiatCurrency { ID = "USD", Name = "US Dollar", Symbol = "$", ApiParameter = "" });
            currencies.Add(new FiatCurrency { ID = "EUR", Name = "Euro", Symbol = "€", ApiParameter = "EUR" });

            return currencies;
        }

        public static FiatCurrency GetFiatCurrencyById(String id)
        {
            return GetAllSupportedCurrencies().Find(x => x.ID == id);
        }
    }
}
