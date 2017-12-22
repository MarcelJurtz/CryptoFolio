using CryptoFolio.Pages;
using CryptoFolio.ServiceHelper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CryptoFolio.ViewModel
{
    public class VMSplashScreen : VM
    {
        ImageManager imageManager;
        APIClient apiClient;
        INavigation navigation;

        public VMSplashScreen(INavigation navigation)
        {
            apiClient = DependencyService.Get<IVM>().GetAPIClient();
            imageManager = DependencyService.Get<IVM>().GetImageManager();
            this.navigation = navigation;
        }

        public void OnAppearing()
        {
            TryLoadingCoinsAsync();
        }

        public ImageSource SplashScreenImage
        {
            get
            {
                return imageManager.GetSplashScreenImage();
            }
        }

        public ImageSource LoadingIndicator
        {
            get
            {
                return imageManager.GetLoadingIndicator();
            }
        }

        public String LoadingText
        {
            get
            {
                return "Loading Coins...";
            }
        }

        public double LoaderMargin
        {
            get
            {
                return 20;
            }
        }

        public async void TryLoadingCoinsAsync()
        {
                DateTime localDate = DependencyService.Get<IVM>().GetLiteDbManager().LoadCoinListLatestChangeDate();
                DateTime currentDate = DateTime.Now;
                DateTime delta = DateTime.Now - TimeSpan.FromDays(1);

                if (DependencyService.Get<IVM>().GetLiteDbManager().LoadCoinListLatestChangeDate() < DateTime.Now - TimeSpan.FromDays(1))
                {
                    var response = await apiClient.GetAllCurrenciesByDefaultFiatCurrencyAsync();
                    DependencyService.Get<IVM>().GetLiteDbManager().SaveCoins(response);
                }

            Task.Delay(10000).Wait();

            await navigation.PushAsync(new OverviewPage());
        }
    }
}
