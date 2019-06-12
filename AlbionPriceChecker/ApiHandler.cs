using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;

namespace AlbionPriceChecker
{
    public static class ApiHandler
    {
        readonly static string apiUrl = "https://www.albion-online-data.com/api/v1/";
        
        //https://www.albion-online-data.com/api/v1/stats/Prices/T1_FARM_CARROT_SEED%2CT4_BAG?locations=lymhurst

        public static List<Item> GetApiPrices(List<Item> items)
        {
            HttpClient client = new HttpClient();

            string formatedItems = "";
            foreach (var item in items)
            {
                formatedItems += "," + item.MetaName;
            }

            string requestUrl = apiUrl + "stats/Prices/" + formatedItems +
                                "?locations=Bridgewatch,Caerleon,Fort Sterling,Lymhurst,Martlock,Thetford";

            var response = client.GetStringAsync(requestUrl).Result;
            var priceResponse = PriceResponse.FromJson(response);

            foreach (var item in items)
            {
                var apiResponseForItems = priceResponse.FindAll(x => x.ItemId == item.MetaName);
                foreach (var apiResponseForItem in apiResponseForItems)
                {
                    if (apiResponseForItem != null)
                    {
                        City city = new City();
                        city.Name = apiResponseForItem.City;
                        city.SellPriceMin = apiResponseForItem.SellPriceMin;
                        city.SellPriceMin = apiResponseForItem.SellPriceMin;
                        city.SellPriceMinDate = apiResponseForItem.SellPriceMinDate;
                        city.BuyPriceMin = apiResponseForItem.BuyPriceMin;
                        city.BuyPriceMinDate = apiResponseForItem.BuyPriceMinDate;
                        item.Cities.Add(city);
                    }
                }   
            }
            return items;
        }
    }
}
