﻿using CryptoFolio.Extensions;
using CryptoFolio.Pages;
using CryptoFolio.ServiceHelper.Base;
using CryptoFolio.ServiceHelper.Values;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CryptoFolio.ViewModel
{
    public class VMCoinDetails : VM, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public VMCoinDetails(INavigation navigation, CoinDTO coin)
        {
            Navigation = navigation;
            _Coin = coin;
            _CoinId = _Coin.Id;
            _Name = String.Format("{0} ({1})", _Coin.Name, _Coin.Symbol);
            _LogoSource = _Coin.Logo;

            _PercentageChanged24h = String.Format("{0} % {1}",_Coin.PercentChange24h, _Coin.PercentChange24h > 0 ? Symbols.ARROW_UP : Symbols.ARROW_DOWN);

            _PlusIconTapCommand = new Command(OnPlusIconTap);

            ItemsLoaded = false;
            IsLoading = true;

            LoadInvestments();
        }

        private INavigation Navigation;
        private CoinDTO _Coin;

        public String Title { get{ return _CoinId.ToFirstLetterUpperCase(); } }

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

        public string DisplayValue
        {
            get
            {
                return _Coin.DisplayValue;
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

        public ImageSource PlusIcon { get { return ImageSource.FromFile("fa_plus.png"); } }

        private ICommand _PlusIconTapCommand;
        public ICommand PlusIconTapCommand { get { return _PlusIconTapCommand; } }

        void OnPlusIconTap()
        {
            Navigation.PushAsync(new InvestmentPage(_Coin));
        }

        private ObservableCollection<Investment> _Investments = new ObservableCollection<Investment>();
        public ObservableCollection<Investment> Investments
        {
            get { return _Investments; }
            set
            {
                if (_Investments != value)
                {
                    _Investments = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Investments)));
                }
            }
        }

        private Investment _SelectedItem;
        public Investment SelectedItem
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

        private void LoadInvestments()
        {
            _Investments = new ObservableCollection<Investment>(DependencyService.Get<IVM>().GetLiteDbManager().LoadInvestmentsByCoinSymbol(_Coin.Symbol));
            ItemsLoaded = true;
            IsLoading = false;
        }
    }
}
