using CryptoFolio.ServiceHelper.Base;
using CryptoFolio.ViewModel;
using Xamarin.Forms;

namespace CryptoFolio
{
    public partial class CoinDetailPage : ContentPage
	{
		public CoinDetailPage(CoinDTO Coin)
		{
			InitializeComponent();
            BindingContext = new VMCoinDetails(Navigation, Coin);
		}
    }
}
