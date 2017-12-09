using CryptoFolio.Extensions;
using CryptoFolio.ServiceHelper.Base;
using CryptoFolio.ServiceHelper.Values;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace CryptoFolio.ViewModel
{
    public class VMCoinDetails : VM, INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        public VMCoinDetails(INavigation navigation, CoinDTO coin)
        {
            _Coin = coin;
            _CoinId = _Coin.Id;
            _Name = String.Format("{0} ({1})", _Coin.Name, _Coin.Symbol);
            _LogoSource = _Coin.Logo;
            _CurrentRate = _Coin.PriceUsd;

            _PercentageChanged24h = String.Format("{0} % {1}",_Coin.PercentChange24h, _Coin.PercentChange24h > 0 ? Symbols.ARROW_UP : Symbols.ARROW_DOWN);
        }

        private CoinDTO _Coin;

        public String Title
        {
            get
            {
                return _CoinId.ToFirstLetterUpperCase();
            }
        }

        private String _CoinId;
        public String CoinId
        {
            get { return _CoinId; }
        }

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        private ImageSource _LogoSource;
        public ImageSource LogoSource
        {
            get
            {
                return _LogoSource;
            }
        }

        private string _CurrentRate;
        public string CurrentRate
        {
            get
            {
                return _CurrentRate;
            }
            set
            {
                if (_CurrentRate != value)
                {
                    _CurrentRate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentRate)));
                }
            }
        }

        private string _PercentageChanged24h;
        public string PercentageChanged24h
        {
            get
            {
                return _PercentageChanged24h;
            }
            set
            {
                if (_PercentageChanged24h != value)
                {
                    _PercentageChanged24h = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PercentageChanged24h)));
                }
            }
        }

        public Color StatusColor
        {
            get
            {
                if (_Coin.PercentChange24h > 0)
                    return ColorValues.COLOR_RATE_UP;
                else
                    return ColorValues.COLOR_RATE_DOWN;
            }
        }
    }
}
