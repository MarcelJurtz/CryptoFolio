using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoFolio.ServiceHelper.Localization
{
    public class LanguageManager
    {
        private Dictionary<String, String> Strings;

        #region Keys

        public const String INIT_LOADING_COINS = "init_loading_coins";

        public const String TITLE_OVERVIEW = "title_overview";
        public const String TITLE_ALL_COINS = "title_all_coins";
        public const String TITLE_INVESTMENT = "title_investment";

        public const String INV_BOUGHT = "inv_bought";
        public const String INV_FOR = "inv_for";
        public const String INV_DATE = "inv_date";
        public const String INV_COMMENT = "inv_comment";

        public const String CMD_SAVE = "cmd_save";
        public const String CMD_CANCEL = "cmd_cancel";

        #endregion

        #region EN

        private Dictionary<String, String> en = new Dictionary<string, string>
        {
            { INIT_LOADING_COINS, "Loading Coins..." },
            { TITLE_OVERVIEW, "Overview" },
            { TITLE_ALL_COINS, "All Coins" },
            { TITLE_INVESTMENT, "New Investment" },
            { INV_BOUGHT, "Bought:" },
            { INV_FOR, "For:" },
            { INV_DATE, "Date:" },
            { INV_COMMENT, "Comment:" },
            { CMD_SAVE, "Save" },
            { CMD_CANCEL, "Cancel" }
        };

        #endregion

        #region DE

        private Dictionary<String, String> de = new Dictionary<string, string>
        {
            { INIT_LOADING_COINS, "Lade Coins..." },
            { TITLE_OVERVIEW, "Übersicht" },
            { TITLE_ALL_COINS, "Alle Coins" },
            { TITLE_INVESTMENT, "Neues Investment" },
            { INV_BOUGHT, "Gekauft:" },
            { INV_FOR, "Für:" },
            { INV_DATE, "Datum:" },
            { INV_COMMENT, "Kommentar:" },
            { CMD_SAVE, "Speichern" },
            { CMD_CANCEL, "Abbruch" }
        };

        #endregion
    }
}
