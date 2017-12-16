using CryptoFolio.ServiceHelper.Base;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoFolio.ServiceHelper.LiteDbHelper
{
    public class CoinList
    {
        [BsonId]
        public int Id { get; set; }
        public List<CoinDTO> Coins { get; set; }
        public DateTime Updated { get; set; }
    }
}
