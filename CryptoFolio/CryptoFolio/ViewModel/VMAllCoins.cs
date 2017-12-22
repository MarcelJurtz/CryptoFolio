using CryptoFolio.ServiceHelper;
using CryptoFolio.ServiceHelper.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace CryptoFolio.ViewModel
{
    public class VMAllCoins : VM, INotifyPropertyChanged
    {
        private INavigation _Navigation;
        APIClient client;

        public VMAllCoins(INavigation navigation)
        {
            _Navigation = navigation;
            _IsLoading = true;


            client = DependencyService.Get<IVM>().GetAPIClient();
            
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;
                    LoadCoinsAsync();
                });
            }
        }

        private async void LoadCoinsAsync()
        {
            DateTime localDate = DependencyService.Get<IVM>().GetLiteDbManager().LoadCoinListLatestChangeDate();
            DateTime currentDate = DateTime.Now;
            DateTime delta = DateTime.Now - TimeSpan.FromDays(1);

            if (DependencyService.Get<IVM>().GetLiteDbManager().LoadCoinListLatestChangeDate() < DateTime.Now - TimeSpan.FromDays(1))
            {
                var response = await client.GetAllCurrenciesByDefaultFiatCurrencyAsync();
                Coins = new ObservableCollection<CoinDTO>(response);

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
