using CryptoFolio.ServiceHelper;
using CryptoFolio.ServiceHelper.Values;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CryptoFolio.ViewModel
{
    public class VMMainPage : VM, INotifyPropertyChanged
    {
        public VMMainPage(INavigation navigation)
        {
            Navigation = navigation;
            _ListIconTapCommand = new Command(OnListIconTap);
            _Currencies = FiatCurrency.GetAllSupportedCurrencies();
            _SelectedCurrency = _Currencies.Find(x => x.ID == PreferenceManager.DefaultCurrencyId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private INavigation Navigation;

        public String TotalOutputString { get { return "5 $"; } }
        public String TotalInputRatioString { get { return "0$ / +0$"; } }

        public ImageSource ListIcon { get { return ImageSource.FromFile("fa_list.png"); } }

        private ICommand _ListIconTapCommand;
        public ICommand ListIconTapCommand { get { return _ListIconTapCommand; } }

        void OnListIconTap()
        {
            Navigation.PushAsync(new AllCoinsPage());
        }

        private List<FiatCurrency> _Currencies;
        public List<FiatCurrency> Currencies
        {
            get { return _Currencies; }
        }

        private FiatCurrency _SelectedCurrency;
        public FiatCurrency SelectedCurrency
        {
            get
            {
                return _SelectedCurrency;
            }
            set
            {
                if(_SelectedCurrency != value)
                {
                    _SelectedCurrency = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCurrency)));
                    PreferenceManager.DefaultCurrencyId = _SelectedCurrency.ID;
                }
            }
        }
    }
}