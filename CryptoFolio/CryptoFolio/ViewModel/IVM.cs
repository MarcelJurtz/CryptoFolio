using CryptoFolio.ServiceHelper;

namespace CryptoFolio.ViewModel
{
    interface IVM
    {
        APIClient GetAPIClient();
        LiteDBManager GetLiteDbManager();
        FiatCurrencyManager GetFiatCurrencyManager();
        ImageManager GetImageManager();
    }
}
