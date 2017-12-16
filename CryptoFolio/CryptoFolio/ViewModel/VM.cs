using CryptoFolio.ServiceHelper;
using CryptoFolio.ViewModel;

[assembly: Xamarin.Forms.Dependency(typeof(VM))]
namespace CryptoFolio.ViewModel
{
    public class VM : IVM
    {
        private LiteDBManager liteDbManager;
        public VM()
        {
            liteDbManager = new LiteDBManager();
        }

        private const int _FONT_SIZE_MAIN_PAGE_HEADING = 32;
        public int FONT_SIZE_MAIN_PAGE_HEADING { get { return _FONT_SIZE_MAIN_PAGE_HEADING; } }

        private const int _FONT_SIZE_MAIN_PAGE_SUBHEADING = 26;
        public int FONT_SIZE_MAIN_PAGE_SUBHEADING { get { return _FONT_SIZE_MAIN_PAGE_SUBHEADING; } }

        public LiteDBManager GetLiteDbManager()
        {
            return liteDbManager;
        }
    }
}
