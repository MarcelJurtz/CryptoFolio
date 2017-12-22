using CryptoFolio.ServiceHelper;
using CryptoFolio.ViewModel;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(VM))]
namespace CryptoFolio.ViewModel
{
    public class VM : IVM
    {
        private APIClient apiClient;
        private LiteDBManager liteDbManager;
        private FiatCurrencyManager fiatCurrencyManager;
        private ImageManager imageManager;

        public VM()
        {
            apiClient = new APIClient();
            liteDbManager = new LiteDBManager();
            fiatCurrencyManager = new FiatCurrencyManager();
            imageManager = new ImageManager();
        }

        private const int _FONT_SIZE_TEXT_REGULAR = 16;
        private const int _FONT_SIZE_MAIN_PAGE_HEADING = 32;
        private const int _FONT_SIZE_MAIN_PAGE_SUBHEADING = 26;

        public LiteDBManager GetLiteDbManager()
        {
            return liteDbManager;
        }

        public FiatCurrencyManager GetFiatCurrencyManager()
        {
            return fiatCurrencyManager;
        }

        public ImageManager GetImageManager()
        {
            return imageManager;
        }

        public APIClient GetAPIClient()
        {
            return apiClient;
        }

        public Color RegularTextColor
        {
            get
            {
                return Color.Black;
            }
        }

        public Color LightTextColor
        {
            get
            {
                return Color.FromHex("#f5f5f5");
            }
        }

        public Color RegularPrimaryColor
        {
            get
            {
                return Color.FromHex("#48c359");
            }
        }

        public double RegularTextSize
        {
            get
            {
                return _FONT_SIZE_TEXT_REGULAR;
            }
        }

        public double SubHeadingTextSize
        {
            get
            {
                return _FONT_SIZE_MAIN_PAGE_SUBHEADING;
            }
        }

        public double HeadingTextSize
        {
            get
            {
                return _FONT_SIZE_MAIN_PAGE_HEADING;
            }
        }
    }
}
