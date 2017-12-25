using CryptoFolio.ServiceHelper;
using CryptoFolio.ServiceHelper.Localization;

namespace CryptoFolio.ViewModel
{
    interface IVM
    {
        APIClient GetAPIClient();
        LiteDBManager GetLiteDbManager();
        FiatCurrencyManager GetFiatCurrencyManager();
        ImageManager GetImageManager();
        LanguageManager GetLanguageManager();
    }
}
