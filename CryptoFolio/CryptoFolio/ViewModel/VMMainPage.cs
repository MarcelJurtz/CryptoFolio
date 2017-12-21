using CryptoFolio.ServiceHelper;
using CryptoFolio.ServiceHelper.Base;
using CryptoFolio.ServiceHelper.Values;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            _Currencies = DependencyService.Get<IVM>().GetFiatCurrencyManager().GetAllSupportedCurrencies();
            _SelectedCurrency = _Currencies.Find(x => x.ID == PreferenceManager.DefaultCurrencyId);

            _ItemsLoaded = false;
            _IsLoading = true;

            LoadAggregatedInvestments();
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
            _AggregatedInvestments = new ObservableCollection<AggregatedInvestment>(DependencyService.Get<IVM>().GetLiteDbManager().LoadAggregatedInvestments());
            ItemsLoaded = true;
            IsLoading = false;
        }
    }
}