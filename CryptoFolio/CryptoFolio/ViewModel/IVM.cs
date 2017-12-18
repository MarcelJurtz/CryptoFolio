using CryptoFolio.ServiceHelper;

namespace CryptoFolio.ViewModel
{
    interface IVM
    {
        LiteDBManager GetLiteDbManager();
        FiatCurrencyManager GetFiatCurrencyManager();
    }
}
