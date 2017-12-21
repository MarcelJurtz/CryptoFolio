using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CryptoFolio.ServiceHelper.Base;
using CryptoFolio.ServiceHelper.LiteDbHelper;
using LiteDB;

namespace CryptoFolio.ServiceHelper
{
    public class LiteDBManager
    {
        private LiteDatabase liteDatabase;
        private const String dbPath = "CoinDatabase.db";
        private const String coinDocument = "coins";
        private const String investmentDocument = "investments";

        public LiteDBManager()
        {
            liteDatabase = new LiteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbPath));
        }

        public void SaveCoins(List<CoinDTO> ApiLoadedCoins)
        {
            var doc = liteDatabase.GetCollection<CoinList>(coinDocument);
            doc.Delete(Query.All());

            CoinList coinDbEntry = new CoinList
            {
                Id = 0,
                Coins = ApiLoadedCoins,
                Updated = DateTime.Now
            };

            doc.Insert(coinDbEntry);
        }

        public List<CoinDTO> LoadCoins()
        {
            var res = liteDatabase.GetCollection<CoinList>(coinDocument).FindById(0);
            return res.Coins;
        }

        public DateTime LoadCoinListLatestChangeDate()
        {
            var res = liteDatabase.GetCollection<CoinList>(coinDocument).FindById(0);
            return res?.Updated ?? DateTime.MinValue;
        }

        public void SaveInvestment(Investment investment)
        {
            var doc = liteDatabase.GetCollection<Investment>(investmentDocument);
            doc.Insert(investment);
        }

        public IEnumerable<Investment> LoadInvestmentsByCoinSymbol(String coinSymbol)
        {
            var doc = liteDatabase.GetCollection<Investment>(investmentDocument);
            return doc.Find(x => x.CryptoCurrencySymbol == coinSymbol);
        }

        public List<AggregatedInvestment> LoadAggregatedInvestments()
        {
            Dictionary<String, AggregatedInvestment> items = new Dictionary<string, AggregatedInvestment>();

            var doc = liteDatabase.GetCollection<Investment>(investmentDocument);
            var query = doc.FindAll();
            foreach(Investment investment in query)
            {
                if(items.ContainsKey(investment.CryptoCurrencySymbol))
                {
                    items[investment.CryptoCurrencySymbol].CryptoCurrencyValue += investment.RevenueInCryptoCurrency;
                    items[investment.CryptoCurrencySymbol].FiatCurrencyInput += investment.RevenueInCryptoCurrency;
                }
                else
                {
                    items.Add(investment.CryptoCurrencySymbol, new AggregatedInvestment
                    {
                        CryptoCurrencySymbol = investment.CryptoCurrencySymbol,
                        CryptoCurrencyValue = investment.RevenueInCryptoCurrency,
                        FiatCurrencyInput = investment.ExpenseInFiatCurrency,
                        FiatCurrencySymbol = investment.FiatCurrencySymbol
                    });
                }
            }

            // Calculate current value
            return items.Values.ToList();
        }
    }
}
