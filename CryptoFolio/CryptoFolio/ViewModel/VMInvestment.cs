using CryptoFolio.ServiceHelper;
using CryptoFolio.ServiceHelper.Base;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CryptoFolio.ViewModel
{
    public class VMInvestment : VM, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public VMInvestment(INavigation navigation, CoinDTO coin)
        {
            this.Coin = coin;
            this.Navigation = navigation;
            _CancelCommand = new Command(OnCancelClick);
            _SaveCommand = new Command(OnSaveClick);
            _TransactionDate = DateTime.Today;
        }

        private CoinDTO Coin;
        private INavigation Navigation;

        public String Header { get { return "New Investment"; } }

        #region Money Received
        public String ReceivedText { get { return "Bought:"; } }

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

        public String SpentText { get { return "For:"; } }

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

        public String TimestampText { get { return "Date:"; } }

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

        public String CommentText { get { return "Comment:"; } }

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

        public String CancelButtonText { get { return "Cancel"; } }
        private ICommand _CancelCommand;
        public ICommand CancelCommand { get { return _CancelCommand; } }

        public String SaveButtonText { get { return "Save"; } }
        private ICommand _SaveCommand;
        public ICommand SaveCommand { get { return _SaveCommand; } }

        private async void OnCancelClick()
        {
            await Navigation.PopAsync();
        }

        private async void OnSaveClick()
        {
            Investment investment = new Investment
            {
                CryptoCurrencySymbol = Coin.Symbol,
                FiatCurrencySymbol = DependencyService.Get<IVM>().GetFiatCurrencyManager().GetDefaultFiatCurrency().Symbol,
                ExpenseInFiatCurrency = _Spent,
                RevenueInCryptoCurrency = _Received,
                TransactionTimestamp = _TransactionDate,
                Comment = _Comment
            };

            LiteDBManager liteDBManager = DependencyService.Get<IVM>().GetLiteDbManager();
            liteDBManager.SaveInvestment(investment);

            await Navigation.PopAsync();
        }

        #endregion
    }
}
