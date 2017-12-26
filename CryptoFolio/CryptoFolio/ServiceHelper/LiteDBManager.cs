using CryptoFolio.ServiceHelper.Base;
using CryptoFolio.ServiceHelper.LiteDbHelper;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CryptoFolio.ServiceHelper
{
    public class LiteDBManager
    {
        private LiteDatabase liteDatabase;
        private const String DB_PATH = "CoinDatabase.db";
        private const String DOC_COIN = "coins";
        private const String DOC_INVESTMENT = "investments";

        public LiteDBManager()
        {
            liteDatabase = new LiteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DB_PATH));
        }

        public void SaveCoins(List<CoinDTO> ApiLoadedCoins)
        {
            var doc = liteDatabase.GetCollection<CoinList>(DOC_COIN);
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
            var doc = liteDatabase.GetCollection<CoinList>(DOC_COIN);
            doc.Delete(x => x.Updated < DateTime.Today);
        }

        public void DeleteAllCoinData()
        {
            var doc = liteDatabase.GetCollection<CoinList>(DOC_COIN);
            doc.Delete(x => x.Updated <= DateTime.Today);
        }

        public List<CoinDTO> LoadCoins()
        {
            var res = liteDatabase.GetCollection<CoinList>(DOC_COIN).FindOne(x => x.Updated == DateTime.Today);
            return res.Coins;
        }

        public DateTime LoadCoinListLatestChangeDate()
        {
            var res = liteDatabase.GetCollection<CoinList>(DOC_COIN).FindOne(x => x.Updated == DateTime.Today);
            return res?.Updated ?? DateTime.MinValue;
        }

        public void SaveInvestment(Investment investment)
        {
            var doc = liteDatabase.GetCollection<Investment>(DOC_INVESTMENT);
            doc.Insert(investment);
        }

        public IEnumerable<Investment> LoadInvestmentsByCoinSymbol(String coinSymbol)
        {
            var doc = liteDatabase.GetCollection<Investment>(DOC_INVESTMENT);
            return doc.Find(x => x.CryptoCurrencySymbol == coinSymbol);
        }

        public List<AggregatedInvestment> LoadAggregatedInvestments(String fiatCurrencyId)
        {
            Dictionary<String, AggregatedInvestment> items = new Dictionary<string, AggregatedInvestment>();

            var investmentDoc = liteDatabase.GetCollection<Investment>(DOC_INVESTMENT);
            var query = investmentDoc.FindAll();
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
            var coinDoc = liteDatabase.GetCollection<CoinList>(DOC_COIN);
            foreach(AggregatedInvestment ai in items.Values)
            {
                var queryCoin = coinDoc.FindOne(x => x.Updated == DateTime.Today);

                switch(fiatCurrencyId)
                {
                    case "USD":
                        ai.FiatCurrencyValue = queryCoin.Coins.Find(x => x.Symbol == ai.CryptoCurrencySymbol).PriceUsd * ai.CryptoCurrencyValue;
                        break;
                    case "EUR":
                        ai.FiatCurrencyValue = queryCoin.Coins.Find(x => x.Symbol == ai.CryptoCurrencySymbol).PriceEur * ai.CryptoCurrencyValue;
                        break;
                    default:
                        ai.FiatCurrencyValue = 0;
                        break;
                }
            }

            return items.Values.OrderByDescending(x => x.FiatCurrencyValue).ToList();
        }
    }
}
