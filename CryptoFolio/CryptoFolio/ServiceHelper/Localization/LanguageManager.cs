using System;
using System.Collections.Generic;
using System.Globalization;

namespace CryptoFolio.ServiceHelper.Localization
{
    public class LanguageManager
    {
        public Dictionary<CryptoFolioStrings, String> GetStringsForDefaultLanguage()
        {
            String defaultLanguageIsoString = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            // ISO Codes: https://www.worldatlas.com/aatlas/ctycodes.htm

            switch (defaultLanguageIsoString)
            {
                case "DE":
                    return STRINGS_DE;
                default:
                    return STRINGS_EN;
            }
        }

        #region Keys

        #endregion

        #region EN

        private Dictionary<CryptoFolioStrings, String> STRINGS_EN = new Dictionary<CryptoFolioStrings, string>
        {
            { CryptoFolioStrings.INIT_LOADING_COINS, "Loading Coins..." },
            { CryptoFolioStrings.TITLE_OVERVIEW, "Overview" },
            { CryptoFolioStrings.TITLE_ALL_COINS, "All Coins" },
            { CryptoFolioStrings.TITLE_INVESTMENT, "New Investment" },
            { CryptoFolioStrings.INV_BOUGHT, "Bought:" },
            { CryptoFolioStrings.INV_FOR, "For:" },
            { CryptoFolioStrings.INV_DATE, "Date:" },
            { CryptoFolioStrings.INV_COMMENT, "Comment:" },
            { CryptoFolioStrings.CMD_SAVE, "Save" },
            { CryptoFolioStrings.CMD_CANCEL, "Cancel" },
            { CryptoFolioStrings.INVESTMENDMODE_BUY, "Bought" },
            { CryptoFolioStrings.INVESTMENTMODE_SELL, "Sold" }
        };

        #endregion

        #region DE

        private Dictionary<CryptoFolioStrings, String> STRINGS_DE = new Dictionary<CryptoFolioStrings, string>
        {
            { CryptoFolioStrings.INIT_LOADING_COINS, "Lade Coins..." },
            { CryptoFolioStrings.TITLE_OVERVIEW, "Übersicht" },
            { CryptoFolioStrings.TITLE_ALL_COINS, "Alle Coins" },
            { CryptoFolioStrings.TITLE_INVESTMENT, "Neues Investment" },
            { CryptoFolioStrings.INV_BOUGHT, "Gekauft:" },
            { CryptoFolioStrings.INV_FOR, "Für:" },
            { CryptoFolioStrings.INV_DATE, "Datum:" },
            { CryptoFolioStrings.INV_COMMENT, "Kommentar:" },
            { CryptoFolioStrings.CMD_SAVE, "Speichern" },
            { CryptoFolioStrings.CMD_CANCEL, "Abbruch" },
            { CryptoFolioStrings.INVESTMENDMODE_BUY, "Kaufe" },
            { CryptoFolioStrings.INVESTMENTMODE_SELL, "Verkaufe" }
        };

        #endregion
    }

    public enum CryptoFolioStrings
    {
        INIT_LOADING_COINS,
        TITLE_OVERVIEW,
        TITLE_ALL_COINS,
        TITLE_INVESTMENT,
        INV_BOUGHT,
        INV_FOR,
        INV_DATE,
        INV_COMMENT,
        CMD_SAVE,
        CMD_CANCEL,
        INVESTMENDMODE_BUY,
        INVESTMENTMODE_SELL
    }
}
