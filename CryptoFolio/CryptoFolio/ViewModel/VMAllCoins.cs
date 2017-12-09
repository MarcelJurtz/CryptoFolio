using CryptoFolio.ServiceHelper;
using CryptoFolio.ServiceHelper.Base;
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

        public VMAllCoins(INavigation navigation)
        {
            _Navigation = navigation;
            APIClient client = new APIClient();
            var response = client.GetAllCurrencies();
            Coins = new ObservableCollection<CoinDTO>(response as List<CoinDTO>);
            _ListCoinTapCommand = new Command(OnListCoinTap);
        }

        private ObservableCollection<CoinDTO> _Coins = new ObservableCollection<CoinDTO>();
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CoinDTO> Coins
        {
            get { return _Coins; }
            set
            {
                _Coins = value;
                OnPropertyChanged();
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
    }
}
