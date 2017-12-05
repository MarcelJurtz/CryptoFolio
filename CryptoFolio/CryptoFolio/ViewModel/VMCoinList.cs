using CryptoFolio.ServiceHelper;
using CryptoFolio.ServiceHelper.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CryptoFolio.ViewModel
{
    public class VMCoinList : VM, INotifyPropertyChanged
    {
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

        public VMCoinList()
        {
            APIClient client = new APIClient();
            var response = client.GetAllCurrencies();
            Coins = new ObservableCollection<CoinDTO>(response as List<CoinDTO>);
        }
    }
}
