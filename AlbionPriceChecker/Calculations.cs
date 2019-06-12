using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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
                totalValue += item.MostAccuratePrice * item.Amount;
            }

            return totalValue;
        }

        public static List<Item> CalculateAccuratePriceFromMultipleCities(List<Item> items, string prioritizedCity)
        {
            foreach (var item in items)
            {
                List<Price> processedPrices = new List<Price>();
                for (int i = 0; i < item.Cities.Count; i++)
                {
                    Price diff = new Price();
                    diff.Value = item.Cities[i].SellPriceMin;
                    diff.City = item.Cities[i];
                    List<long> prices = new List<long>();
                    foreach (var curCity in item.Cities)
                    {
                        if (diff.CompareWith(curCity.SellPriceMin))
                        {
                            diff.SimiliarTo.Add(curCity.SellPriceMin);
                        }
                    }
                    processedPrices.Add(diff);
                }

                Price accuratePrice = new Price();
                foreach (var price in processedPrices)
                {
                    if (price.SimiliarTo.Count > accuratePrice.SimiliarTo.Count)
                    {
                        accuratePrice = price;
                    }
                }

                Price prioCity = processedPrices.Find(x => x.City.Name.ToUpper() == prioritizedCity);
                if (prioCity != null && prioCity.SimiliarTo.Count >= accuratePrice.SimiliarTo.Count)
                {
                    item.MostAccuratePrice = prioCity.Value;
                    item.MostAccurateCity = prioCity.City;
                }
                else
                {
                    item.MostAccuratePrice = accuratePrice.Value;
                    item.MostAccurateCity = accuratePrice.City;
                }
            }

            return items;
        }
    }

    public class Price
    {
        public long Value { get; set; }
        public City City { get; set; }

        public List<long> SimiliarTo = new List<long>();

        public bool CompareWith(long value)
        {
            if (Math.Abs(this.Value - value) <= GetPercentage(30, this.Value) && this.Value < 9999999)
            {
                return true;
            }

            return false;
        }
        private static long GetPercentage(int percentage, long value)
        {
            return (value * percentage) / 100;
        }
    }
}
