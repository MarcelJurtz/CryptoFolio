using LiteDB;
using System;

namespace CryptoFolio.ServiceHelper.Base
{
    public class Investment
    {
        [BsonId]
        public int Id { get; set; }
        public String CryptoCurrencySymbol { get; set; }
        public decimal Expense { get; set; }
        public String FiatCurrencySymbol { get; set; } 
        public decimal Revenue { get; set; }
        public DateTime TransactionTimestamp { get; set; }
        public String Comment { get; set; }
        public InvestmentModes Mode { get; set; }

        [BsonIgnore]
        public String FormattedDescription
        {
            get
            {
                if(Mode == InvestmentModes.BUY_CRYPTO)
                {
                    return $"{TransactionTimestamp.ToString("dd.MM.yyyy")}: + {Revenue} {CryptoCurrencySymbol} / - {Expense} {FiatCurrencySymbol}";
                }
                else
                {
                    return $"{TransactionTimestamp.ToString("dd.MM.yyyy")}: - {Expense} {CryptoCurrencySymbol} / + {Revenue} {FiatCurrencySymbol}";
                }
            }
        }

        [BsonIgnore]
        public String FormattedTimestamp
        {
            get
            {
                return TransactionTimestamp.ToString("dd.MM.yyyy");
            }
        }
    }

    public enum InvestmentModes
    {
        BUY_CRYPTO,
        SELL_CRYPTO
    }
}
