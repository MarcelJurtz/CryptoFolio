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
            var res = doc.FindOne(x => x.Updated == DateTime.Today);
            if(res != null)
            {
                res.Coins = ApiLoadedCoins;
                doc.Update(res);
            }
            else
            {
                CoinList coinDbEntry = new CoinList
                {
                    Coins = ApiLoadedCoins,
                    Updated = DateTime.Today
                };

                doc.Insert(coinDbEntry);
            }
        }

        public void DeleteOldCoinData()
        {
            var doc = liteDatabase.GetCollection<CoinList>(coinDocument);
            doc.Delete(x => x.Updated < DateTime.Today);
        }

        public List<CoinDTO> LoadCoins()
        {
            var res = liteDatabase.GetCollection<CoinList>(coinDocument).FindOne(x => x.Updated == DateTime.Today);
            return res.Coins;
        }

        public DateTime LoadCoinListLatestChangeDate()
        {
            var res = liteDatabase.GetCollection<CoinList>(coinDocument).FindOne(x => x.Updated == DateTime.Today);
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
            return items.Values.OrderByDescending(x => x.FiatCurrencyValue).ToList();
        }
    }
}
