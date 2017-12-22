using CryptoFolio.ViewModel;
using Xamarin.Forms;

namespace CryptoFolio.Pages
{
    public partial class SplashscreenPage : ContentPage
	{

        private VMSplashScreen viewModel;

		public SplashscreenPage()
		{
			InitializeComponent();
            viewModel = new VMSplashScreen(Navigation);
            BindingContext = viewModel;
		}

        protected override void OnAppearing()
        {
            viewModel.OnAppearing();
            base.OnAppearing();
        }
    }
}
