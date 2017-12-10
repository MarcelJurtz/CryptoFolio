using System;

namespace CryptoFolio.ServiceHelper.Base
{
    public class Investment
    {
        public String CryptoCurrencySymbol { get; set; }
        public decimal ExpenseInDefaultCurrency { get; set; }
        public decimal RevenueInCryptoCurrency { get; set; }
        public DateTime TransactionTimestamp { get; set; }
    }
}
