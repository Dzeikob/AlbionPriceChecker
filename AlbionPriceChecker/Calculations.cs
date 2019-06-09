using System;
using System.Collections.Generic;
using System.Text;

namespace AlbionPriceChecker
{
    public static class Calculations
    {
        public static long CalculateTotalValue(List<Item> items)
        {
            long totalValue = 0;
            foreach (var item in items)
            {
                totalValue += item.SellPriceMin * item.Amount;
            }

            return totalValue;
        }
    }
}
