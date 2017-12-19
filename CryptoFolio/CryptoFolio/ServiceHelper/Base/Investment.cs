using LiteDB;
using System;

namespace CryptoFolio.ServiceHelper.Base
{
    public class Investment
    {
        [BsonId]
        public int Id { get; set; }
        public String CryptoCurrencySymbol { get; set; }
        public decimal ExpenseInFiatCurrency { get; set; }
        public String FiatCurrencySymbol { get; set; } 
        public decimal RevenueInCryptoCurrency { get; set; }
        public DateTime TransactionTimestamp { get; set; }
        public String Comment { get; set; }

        [BsonIgnore]
        public String FormattedDescription
        {
            get
            {
                return $"Bought {RevenueInCryptoCurrency} {CryptoCurrencySymbol} for {ExpenseInFiatCurrency} {FiatCurrencySymbol}";
            }
        }
    }
}
