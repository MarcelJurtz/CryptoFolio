using CryptoFolio.Pages;
using Xamarin.Forms;

namespace CryptoFolio
{
    public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new SplashscreenPage())
            {
                BarBackgroundColor = Color.FromHex("#48c259"),
                BarTextColor = Color.FromHex("#f5f5f5")
            };
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
