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
            TryLoadCoinsAsync();
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

        public async void TryLoadCoinsAsync()
        {
            DateTime localDate = DependencyService.Get<IVM>().GetLiteDbManager().LoadCoinListLatestChangeDate();

            if (DependencyService.Get<IVM>().GetLiteDbManager().LoadCoinListLatestChangeDate() < DateTime.Today)
            {
                var response = await apiClient.GetAllCurrenciesByDefaultFiatCurrencyAsync();
                DependencyService.Get<IVM>().GetLiteDbManager().SaveCoins(response);
            }

            await navigation.PushAsync(new OverviewPage());
        }
    }
}
