using CryptoFolio.ServiceHelper.Base;
using CryptoFolio.ViewModel;
using Xamarin.Forms;

namespace CryptoFolio.Pages
{
    public partial class InvestmentPage : ContentPage
	{
        public InvestmentPage(CoinDTO Coin)
		{
			InitializeComponent();
            BindingContext = new VMInvestment(Navigation, Coin);
		}
    }
}
