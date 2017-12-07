using CryptoFolio.ViewModel;
using Xamarin.Forms;

namespace CryptoFolio
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            BindingContext = new VMMainPage(Navigation);
		}
    }
}
