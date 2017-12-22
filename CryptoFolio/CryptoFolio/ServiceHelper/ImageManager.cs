using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CryptoFolio.ServiceHelper
{
    public class ImageManager
    {
        private Dictionary<String, String> IdSymbolMap = new Dictionary<string, string>
        {
            { "bitcoin" , "BTC" },
            { "ethereum" , "ETH" },
            { "bitcoin-cash" , "BCH" },
            { "ripple" , "XRP" },
            { "litecoin" , "LTC" },
            { "iota" , "MIOTA" },
            { "cardano" , "ADA" },
            { "dash" , "DASH" },
            { "nem" , "XEM" },
            { "bitcoin-gold" , "BTG" },
            { "monero" , "XMR" },
            { "eos" , "EOS" },
            { "qtum" , "QTUM" },
            { "neo" , "NEO" },
            { "stellar" , "XLM" },
            { "ethereum-classic" , "ETC" },
            { "tron" , "TRX" },
            { "lisk" , "LSK" },
            { "bitconnect" , "BCC" },
            { "zcash" , "ZEC" },
            { "verge" , "XVG" },
            { "omisego" , "OMG" },
            { "populous" , "PPT" },
            { "bitshares" , "BTS" },
            { "waves" , "WAVES" },
            { "hshare" , "HSR" },
            { "tether" , "USDT" },
            { "stratis" , "STRAT" },
            { "ardor" , "ARDR" },
            { "komodo" , "KMD" },
            { "nxt" , "NXT" },
            { "bytecoin-bcn" , "BCN" },
            { "steem" , "STEEM" },
            { "augur" , "REP" },
            { "dogecoin" , "DOGE" },
            { "monacoin" , "MONA" },
            { "ark" , "ARK" },
            { "siacoin" , "SC" },
            { "veritaseum" , "VERI" },
            { "raiblocks" , "XRB" },
            { "decred" , "DCR" },
            { "status" , "SNT" },
            { "golem-network-tokens" , "GNT" },
            { "pivx" , "PIVX" },
            { "binance-coin" , "BNB" },
            { "electroneum" , "ETN" },
            { "salt" , "SALT" },
            { "digibyte" , "DGB" },
            { "power-ledger" , "POWR" },
            { "byteball" , "GBYTE" },
            { "vechain" , "VET" },
            { "bytom" , "BTM" },
            { "walton" , "WTC" },
            { "basic-attention-token" , "BAT" },
            { "vertcoin" , "VTC" },
            { "tenx" , "PAY" },
            { "maidsafecoin" , "MAID" },
            { "factom" , "FCT" },
            { "digixdao" , "DGD" },
            { "kyber-network" , "KNC" },
            { "syscoin" , "SYS" },
            { "aeternity" , "AE" },
            { "zcoin" , "XZC" },
            { "qash" , "QASH" },
            { "bitcoindark" , "BTCD" },
            { "reddcoin" , "RDD" },
            { "0x" , "ZRX" },
            { "gas" , "GAS" },
            { "einsteinium" , "EMC2" },
            { "gamecredits" , "GAME" },
            { "decentraland" , "MANA" },
            { "bitbay" , "BAY" },
            { "funfair" , "FUN" },
            { "santiment" , "SAN" },
            { "gxshares" , "GXS" },
            { "aion" , "AION" },
            { "iconomi" , "ICN" },
            { "civic" , "CVC" },
            { "dragonchain" , "DRGN" },
            { "cryptonex" , "CNX" },
            { "ethos" , "ETHOS" },
            { "gnosis-gno" , "GNO" },
            { "nexus" , "NXS" },
            { "monaco" , "MCO" },
            { "metal" , "MTL" },
            { "raiden-network-token" , "RDN" },
            { "request-network" , "REQ" },
            { "bancor" , "BNT" },
            { "storj" , "STORJ" },
            { "nav-coin" , "NAV" },
            { "blocknet" , "BLOCK" },
            { "streamr-datacoin" , "DATA" },
            { "paypie" , "PPP" },
            { "groestlcoin" , "GRS" },
            { "rchain" , "RHOC" },
            { "substratum" , "SUB" },
            { "ubiq" , "UBQ" },
            { "chainlink" , "LINK" },
            { "asch" , "XAS" },
            { "edgeless" , "EDG" }
        };

        private String GetSymbolById(String id)
        {
            return IdSymbolMap[id];
        }

        private String GetIdBySymbol(String symbol)
        {
            return IdSymbolMap.FirstOrDefault(x => x.Value == symbol).Key;
        }

        public ImageSource GetImageById(String id)
        {
            return ImageSource.FromFile(id.Equals("0x") ? GetSymbolById(id).ToLower().Replace('-', '_') : id.Replace('-', '_'));
        }

        public ImageSource GetImageBySymbol(String symbol)
        {
            String id = GetIdBySymbol(symbol);
            return ImageSource.FromFile(id.Equals("0x") ? symbol.ToLower().Replace('-', '_') : id.Replace('-', '_'));
        }

        public ImageSource GetSplashScreenImage()
        {
            return ImageSource.FromFile("app_splashscreen");
        }

        public ImageSource GetLoadingIndicator()
        {
            return ImageSource.FromFile("app_loading_indicator");
        }
    }
}
