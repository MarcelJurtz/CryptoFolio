using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoFolio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllCoinsPage : ContentPage
	{
		public AllCoinsPage()
		{
			InitializeComponent();
            BindingContext = new ViewModel.VMAllCoins(Navigation);
		}
	}
}