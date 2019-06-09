using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AlbionPriceChecker
{
    public static class ApiHandler
    {
        readonly static string apiUrl = "https://www.albion-online-data.com/api/v1/";
        
        //https://www.albion-online-data.com/api/v1/stats/Prices/T1_FARM_CARROT_SEED%2CT4_BAG?locations=lymhurst

        public static List<Item> GetApiPrices(List<Item> items, string city)
        {
            HttpClient client = new HttpClient();

            string formatedItems = "";
            foreach (var item in items)
            {
                formatedItems += "," + item.MetaName;
            }
            string requestUrl = apiUrl + "stats/Prices/" + formatedItems + "?locations=" + city;

            var response = client.GetStringAsync(requestUrl).Result;
            var priceResponse = PriceResponse.FromJson(response);

            foreach (var item in items)
            {
                var apiResponseForItem = priceResponse.Find(x => x.ItemId == item.MetaName);
                if (apiResponseForItem != null)
                {
                    item.SellPriceMin = apiResponseForItem.SellPriceMin;
                    item.SellPriceMinDate = apiResponseForItem.SellPriceMinDate;
                    item.BuyPriceMin = apiResponseForItem.BuyPriceMin;
                    item.BuyPriceMinDate = apiResponseForItem.BuyPriceMinDate;
                }
            }

            return items;
        }
    }
}
