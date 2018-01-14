using CryptoFolio.ServiceHelper;
using CryptoFolio.ServiceHelper.Base;
using CryptoFolio.ServiceHelper.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CryptoFolio.ViewModel
{
    public class VMInvestment : VM, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Dictionary<CryptoFolioStrings, String> strings;

        public VMInvestment(INavigation navigation, CoinDTO coin)
        {
            this.Coin = coin;
            this.Navigation = navigation;
            strings = DependencyService.Get<IVM>().GetLanguageManager().GetStringsForDefaultLanguage();
            _CancelCommand = new Command(OnCancelClick);
            _SaveCommand = new Command(OnSaveClick);
            _TransactionDate = DateTime.Today;
            _SelectedInvestmentMode = strings[CryptoFolioStrings.INVESTMENDMODE_BUY];
        }

        private CoinDTO Coin;
        private INavigation Navigation;

        public String Title { get { return strings[CryptoFolioStrings.TITLE_INVESTMENT]; } }

        public String[] InvestmentModes
        {
            get
            {
                return new String[] { strings[CryptoFolioStrings.INVESTMENDMODE_BUY], strings[CryptoFolioStrings.INVESTMENTMODE_SELL] };
            }
        }

        private String _SelectedInvestmentMode;
        public String SelectedInvestmentMode
        {
            get
            {
                return _SelectedInvestmentMode;
            }
            set
            {
                if(_SelectedInvestmentMode != value)
                {
                    _SelectedInvestmentMode = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedInvestmentMode)));
                }
            }
        }

        #region Money Received
        public String ReceivedText { get { return strings[CryptoFolioStrings.INV_BOUGHT]; } }

        private decimal _Received = 0;
        public String Received
        {
            get
            {
                return _Received.ToString();
            }
            set
            {
                if(String.IsNullOrEmpty(value))
                {
                    _Received = 0;
                }
                else if(_Received != decimal.Parse(value))
                {
                    _Received = decimal.Parse(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Received)));
                }
            }
        }

        public String ReceivedCurrencySymbol { get { return Coin.Symbol; } }

        #endregion

        #region Money Spent

        public String SpentText { get { return strings[CryptoFolioStrings.INV_FOR]; } }

        private decimal _Spent = 0;
        public String Spent
        {
            get
            {
                return _Spent.ToString();
            }
            set
            {
                if(String.IsNullOrEmpty(value))
                {
                    _Spent = 0;
                }
                else if (_Spent != decimal.Parse(value))
                {
                    _Spent = decimal.Parse(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Spent)));
                }
            }
        }

        public String SpentCurrencySymbol { get { return DependencyService.Get<IVM>().GetFiatCurrencyManager().GetDefaultFiatCurrency().Symbol; } }

        #endregion

        #region Transaction Timestamp

        public String TimestampText { get { return strings[CryptoFolioStrings.INV_DATE]; } }

        private DateTime _TransactionDate;
        public DateTime TransactionDate
        {
            get
            {
                return _TransactionDate;
            }
            set
            {
                if(_TransactionDate != value)
                {
                    _TransactionDate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TransactionDate)));
                }
            }
        }

        #endregion

        #region Comment

        public String CommentText { get { return strings[CryptoFolioStrings.INV_COMMENT]; } }

        private String _Comment;
        public String Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                if(_Comment != value)
                {
                    _Comment = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Comment)));
                }
            }
        }

        #endregion

        #region Buttons OK / Cancel

        public String CancelButtonText { get { return strings[CryptoFolioStrings.CMD_CANCEL]; } }
        private ICommand _CancelCommand;
        public ICommand CancelCommand { get { return _CancelCommand; } }

        public String SaveButtonText { get { return strings[CryptoFolioStrings.CMD_SAVE]; } }
        private ICommand _SaveCommand;
        public ICommand SaveCommand { get { return _SaveCommand; } }

        private async void OnCancelClick()
        {
            await Navigation.PopAsync();
        }

        private async void OnSaveClick()
        {
            Investment investment;
            if(_SelectedInvestmentMode == strings[CryptoFolioStrings.INVESTMENDMODE_BUY])
            {
                investment = new Investment
                {
                    CryptoCurrencySymbol = Coin.Symbol,
                    FiatCurrencySymbol = DependencyService.Get<IVM>().GetFiatCurrencyManager().GetDefaultFiatCurrency().Symbol,
                    Expense = _Spent,
                    Revenue = _Received,
                    TransactionTimestamp = _TransactionDate,
                    Comment = _Comment,
                    Mode = ServiceHelper.Base.InvestmentModes.BUY_CRYPTO
                };
            }
            else
            {
                investment = new Investment
                {
                    CryptoCurrencySymbol = Coin.Symbol,
                    FiatCurrencySymbol = DependencyService.Get<IVM>().GetFiatCurrencyManager().GetDefaultFiatCurrency().Symbol,
                    Expense = _Received,
                    Revenue = _Spent,
                    TransactionTimestamp = _TransactionDate,
                    Comment = _Comment,
                    Mode = ServiceHelper.Base.InvestmentModes.SELL_CRYPTO
                };
            }           

            LiteDBManager liteDBManager = DependencyService.Get<IVM>().GetLiteDbManager();
            liteDBManager.SaveInvestment(investment);

            await Navigation.PopAsync();
        }

        #endregion
    }
}
