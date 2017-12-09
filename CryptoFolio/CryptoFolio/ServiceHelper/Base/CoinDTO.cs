using CryptoFolio.ServiceHelper.Values;
using System;
using System.Runtime.Serialization;
using Xamarin.Forms;

namespace CryptoFolio.ServiceHelper.Base
{
    [DataContract]
    public class CoinDTO
    {
        [DataMember(Name = "id")]
        public String Id { get; set; }

        [DataMember(Name = "name")]
        public String Name { get; set; }

        [DataMember(Name = "symbol")]
        public String Symbol { get; set; }

        [DataMember(Name = "rank")]
        public String Rank { get; set; }

        [DataMember(Name = "price_usd")]
        public String PriceUsd { get; set; }

        [DataMember(Name = "price_btc")]
        public String PriceBtc { get; set; }

        [DataMember(Name = "24h_volume_usd")]
        public String VolumeUsd24h { get; set; }

        [DataMember(Name = "market_cap_usd")]
        public String MarketCapUsd { get; set; }

        [DataMember(Name = "available_supply")]
        public String AvailableSupply { get; set; }

        [DataMember(Name = "total_supply")]
        public String TotalSupply { get; set; }

        [DataMember(Name = "percent_change_1h")]
        public Decimal PercentChange1h { get; set; }

        [DataMember(Name = "percent_change_24h")]
        public Decimal PercentChange24h { get; set; }

        [DataMember(Name = "percent_change_7d")]
        public String PercentChange7d { get; set; }

        [DataMember(Name = "last_updated")]
        public String LastUpdated { get; set; }

        [IgnoreDataMember]
        public String MainDisplayContent
        {
            get
            {
                return String.Format("{0} $, {1}% (24h)", PriceUsd, PercentChange24h);
            }
        }

        [IgnoreDataMember]
        public Color StatusColor
        {
            get
            {
                if (PercentChange24h > 0)
                    return ColorValues.COLOR_RATE_UP;
                else
                    return ColorValues.COLOR_RATE_DOWN;
            }
        }

        [IgnoreDataMember]
        public ImageSource Logo
        {
            get
            {
                return ImageSource.FromFile(Id.Replace('-','_'));
            }
        }
    }
}
