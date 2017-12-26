using CryptoFolio.ViewModel;
using System;
using Xamarin.Forms;

namespace CryptoFolio.ServiceHelper.Base
{
    public class AggregatedInvestment
    {
        public String CryptoCurrencySymbol { get; set; }
        public decimal CryptoCurrencyValue { get; set; }
        public String FiatCurrencySymbol { get; set; }
        public decimal FiatCurrencyValue { get; set; }
        public decimal FiatCurrencyInput { get; set; }

        public String FormattedHeader {
            get
            {
                return $"{CryptoCurrencyValue} {CryptoCurrencySymbol}";
            }
        }

        public String FormattedContent
        {
            get
            {
                return $"{String.Format("{0:n}", FiatCurrencyValue)} {FiatCurrencySymbol} / {String.Format("{0:n}", FiatCurrencyInput)} {FiatCurrencySymbol}";
            }
        }

        public ImageSource Logo
        {
            get
            {
                return DependencyService.Get<IVM>().GetImageManager().GetImageBySymbol(CryptoCurrencySymbol);
            }
        }
    }
}
