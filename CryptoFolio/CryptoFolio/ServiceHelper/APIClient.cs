using CryptoFolio.ServiceHelper.Base;
using CryptoFolio.ServiceHelper.Values;
using CryptoFolio.ViewModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CryptoFolio.ServiceHelper
{
    public class APIClient
    {
        private const String SERVICE_URL = "https://api.coinmarketcap.com/v1/ticker/";

        public APIClient()
        {

        }

        public List<CoinDTO> GetAllCurrencies()
        {
            List<CoinDTO> response;
            using (var client = new JsonServiceClient(SERVICE_URL))
            {
                response = client.Get<List<CoinDTO>>("/");
            }
            return response;
        }

        public List<CoinDTO> GetAllCurrenciesByDefaultFiatCurrency()
        {
            List<CoinDTO> response;
            using (var client = new JsonServiceClient(SERVICE_URL))
            {
                String path = PreferenceManager.DefaultCurrencyId == "USD" ? "/" : "/?convert=" + DependencyService.Get<IVM>().GetFiatCurrencyManager().GetFiatCurrencyById(PreferenceManager.DefaultCurrencyId).ApiConvertAppendix;
                response = client.Get<List<CoinDTO>>(path);
            }
            return response;
        }

        public Task<List<CoinDTO>> GetAllCurrenciesByDefaultFiatCurrencyAsync()
        {
            using(var client = new JsonServiceClient(SERVICE_URL))
            {
                String path = PreferenceManager.DefaultCurrencyId == "USD" ? "/" : "/?convert=" + DependencyService.Get<IVM>().GetFiatCurrencyManager().GetFiatCurrencyById(PreferenceManager.DefaultCurrencyId).ApiConvertAppendix;
                return client.GetAsync<List<CoinDTO>>(path);
            }
        }
    }
}
