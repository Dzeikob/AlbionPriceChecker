namespace AlbionPriceChecker
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PriceResponse
    {
        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("quality")]
        public long Quality { get; set; }

        [JsonProperty("sell_price_min")]
        public long SellPriceMin { get; set; }

        [JsonProperty("sell_price_min_date")]
        public DateTimeOffset SellPriceMinDate { get; set; }

        [JsonProperty("sell_price_max")]
        public long SellPriceMax { get; set; }

        [JsonProperty("sell_price_max_date")]
        public DateTimeOffset SellPriceMaxDate { get; set; }

        [JsonProperty("buy_price_min")]
        public long BuyPriceMin { get; set; }

        [JsonProperty("buy_price_min_date")]
        public DateTimeOffset BuyPriceMinDate { get; set; }

        [JsonProperty("buy_price_max")]
        public long BuyPriceMax { get; set; }

        [JsonProperty("buy_price_max_date")]
        public DateTimeOffset BuyPriceMaxDate { get; set; }
    }

    public partial class PriceResponse
    {
        public static List<PriceResponse> FromJson(string json) => JsonConvert.DeserializeObject<List<PriceResponse>>(json, AlbionPriceChecker.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<PriceResponse> self) => JsonConvert.SerializeObject(self, AlbionPriceChecker.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
