using CryptoFolio.ViewModel;
using Xamarin.Forms;

namespace CryptoFolio.Pages
{
    public partial class OverviewPage : ContentPage
	{
		public OverviewPage()
		{
			InitializeComponent();
            BindingContext = new VMOverview(Navigation);
		}
    }
}
