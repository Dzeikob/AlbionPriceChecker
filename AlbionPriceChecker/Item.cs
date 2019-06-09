using System;
using System.Collections.Generic;
using System.Text;

namespace AlbionPriceChecker
{
    public class Item
    {
        public string Name { get; set; }
        public int Enchantment { get; set; }
        public int Quality { get; set; }
        public int Amount { get; set; }

        public string MetaName { get; set; }

        public long SellPriceMin { get; set; }
        public DateTimeOffset SellPriceMinDate { get; set; }

        public long BuyPriceMin { get; set; }
        public DateTimeOffset BuyPriceMinDate { get; set; }

    }
}
