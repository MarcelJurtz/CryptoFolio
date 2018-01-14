using CryptoFolio.ServiceHelper;
using CryptoFolio.ServiceHelper.Base;
using CryptoFolio.ServiceHelper.Localization;
using CryptoFolio.ServiceHelper.Values;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CryptoFolio.ViewModel
{
    public class VMOverview : VM, INotifyPropertyChanged
    {
        Dictionary<CryptoFolioStrings, String> strings;

        public VMOverview(INavigation navigation)
        {
            Navigation = navigation;
            _ListIconTapCommand = new Command(OnListIconTap);
            _Currencies = DependencyService.Get<IVM>().GetFiatCurrencyManager().GetAllSupportedCurrencies();
            _SelectedCurrency = _Currencies.Find(x => x.ID == PreferenceManager.DefaultCurrencyId);

            _ItemsLoaded = false;
            _IsLoading = true;

            strings = DependencyService.Get<IVM>().GetLanguageManager().GetStringsForDefaultLanguage();

            _CurrencyPickerEnabled = true;

            LoadAggregatedInvestments();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private INavigation Navigation;

        private decimal _TotalOutput;
        public String TotalOutputString
        {
            get
            {
                return $"{String.Format("{0:n}", _TotalOutput)} {DependencyService.Get<IVM>().GetFiatCurrencyManager().GetDefaultFiatCurrency().Symbol}";
            }
        }

        private String _percentage;
        private decimal _TotalInput;
        public String TotalInputString
        {
            get
            {
                return $"{String.Format("{0:n}", _TotalInput)} {DependencyService.Get<IVM>().GetFiatCurrencyManager().GetDefaultFiatCurrency().Symbol} / {_percentage} %";
            }
        }

        public ImageSource ListIcon { get { return ImageSource.FromFile("fa_list.png"); } }

        private ICommand _ListIconTapCommand;
        public ICommand ListIconTapCommand { get { return _ListIconTapCommand; } }

        void OnListIconTap()
        {
            Navigation.PushAsync(new AllCoinsPage());
        }

        public String Title
        {
            get
            {
                return strings[CryptoFolioStrings.TITLE_OVERVIEW];
            }
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
                if (_SelectedCurrency != value)
                {
                    _SelectedCurrency = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCurrency)));
                    PreferenceManager.DefaultCurrencyId = _SelectedCurrency.ID;

                    // Reload Coins for new Currency
                    DependencyService.Get<IVM>().GetLiteDbManager().DeleteAllCoinData();

                    Navigation.PopAsync();
                }
            }
        }

        private ObservableCollection<AggregatedInvestment> _AggregatedInvestments = new ObservableCollection<AggregatedInvestment>();
        public ObservableCollection<AggregatedInvestment> AggregatedInvestments
        {
            get { return _AggregatedInvestments; }
            set
            {
                if (_AggregatedInvestments != value)
                {
                    _AggregatedInvestments = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AggregatedInvestments)));
                }
            }
        }

        private AggregatedInvestment _SelectedItem;
        public AggregatedInvestment SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;

                if (_SelectedItem == null)
                    return;

                //ListCoinTapCommand.Execute(_SelectedItem);

                SelectedItem = null;
            }
        }

        private bool _CurrencyPickerEnabled;
        public bool CurrencyPickerEnabled
        {
            get
            {
                return _CurrencyPickerEnabled;
            }
        }

        private bool _IsLoading;
        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                if (_IsLoading != value)
                {
                    _IsLoading = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoading)));
                }
            }
        }

        private bool _ItemsLoaded;
        public bool ItemsLoaded
        {
            get
            {
                return _ItemsLoaded;
            }
            set
            {
                if (_ItemsLoaded != value)
                {
                    _ItemsLoaded = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemsLoaded)));
                }
            }
        }

        private void LoadAggregatedInvestments()
        {
            _AggregatedInvestments = new ObservableCollection<AggregatedInvestment>(DependencyService.Get<IVM>().GetLiteDbManager().LoadAggregatedInvestments(PreferenceManager.DefaultCurrencyId));

            if (_AggregatedInvestments.Count > 0)
            {
                _CurrencyPickerEnabled = false;

                foreach (AggregatedInvestment ai in _AggregatedInvestments)
                {
                    _TotalInput += ai.FiatCurrencyInput;
                    _TotalOutput += ai.FiatCurrencyValue;
                }

                decimal percentage = _TotalInput == 0 ? 0 : _TotalOutput / _TotalInput * 100;
                _percentage = percentage > 0 ? "+ " + String.Format("{0:n}", percentage) : String.Format("{0:n}", percentage);
            }
            else
            {
                _percentage = String.Format("{0:n}", 0);
                _TotalInput = 0m;
                _TotalInput = 0m;
            }
            ItemsLoaded = true;
            IsLoading = false;
        }
    }
}