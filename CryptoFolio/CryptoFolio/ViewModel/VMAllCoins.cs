using CryptoFolio.ServiceHelper;
using CryptoFolio.ServiceHelper.Base;
using CryptoFolio.ServiceHelper.Localization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CryptoFolio.ViewModel
{
    public class VMAllCoins : VM, INotifyPropertyChanged
    {
        private INavigation _Navigation;
        APIClient client;
        Dictionary<CryptoFolioStrings, String> strings;

        public VMAllCoins(INavigation navigation)
        {
            _Navigation = navigation;
            _IsLoading = true;


            client = DependencyService.Get<IVM>().GetAPIClient();
            strings = DependencyService.Get<IVM>().GetLanguageManager().GetStringsForDefaultLanguage();
            
            _ListCoinTapCommand = new Command(OnListCoinTap);

            LoadCoinsAsync();
        }

        private ObservableCollection<CoinDTO> _Coins = new ObservableCollection<CoinDTO>();
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CoinDTO> Coins
        {
            get { return _Coins; }
            set
            {
                if(_Coins != value)
                {
                    _Coins = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Coins)));
                }
            }
        }

        public String Title { get { return strings[CryptoFolioStrings.TITLE_ALL_COINS]; } }

        private ICommand _ListCoinTapCommand;
        public ICommand ListCoinTapCommand { get { return _ListCoinTapCommand; } }

        private void OnListCoinTap()
        {
            _Navigation.PushAsync(new CoinDetailPage(SelectedItem));
        }

        private CoinDTO _SelectedItem;
        public CoinDTO SelectedItem
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

                ListCoinTapCommand.Execute(_SelectedItem);

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
                if(_IsLoading != value)
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
                if(_ItemsLoaded != value)
                {
                    _ItemsLoaded = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemsLoaded)));
                }
            }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;
                    LoadCoinsAsync(true);
                });
            }
        }

        private async void LoadCoinsAsync(bool forceReload = false)
        {
            if (DependencyService.Get<IVM>().GetLiteDbManager().LoadCoinListLatestChangeDate() < DateTime.Today || forceReload)
            {
                var response = await client.GetAllCurrenciesByDefaultFiatCurrencyAsync();
                Coins = new ObservableCollection<CoinDTO>(response);

                DependencyService.Get<IVM>().GetLiteDbManager().DeleteOldCoinData();
                DependencyService.Get<IVM>().GetLiteDbManager().SaveCoins(response);
            }
            else
            {
                Coins = new ObservableCollection<CoinDTO>(DependencyService.Get<IVM>().GetLiteDbManager().LoadCoins());
            }
            
            ItemsLoaded = true;
            IsLoading = false;
            IsRefreshing = false;
        }
    }
}
