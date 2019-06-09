using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AlbionPriceChecker
{
    public class MetaItem
    {
        [JsonProperty(PropertyName = "Index")]
        public int Index { get; set; }
        
        [JsonProperty(PropertyName = "LocalizedNames")]
        public List<LocalizedNames> LocalizedNames { get; set; }

        [JsonProperty(PropertyName = "LocalizationNameVariable")]
        public string LocalizationNameVariable { get; set; }

        [JsonProperty(PropertyName = "UniqueName")]
        public string UniqueName { get; set; }
    }
    public partial class LocalizedNames
    {
        [JsonProperty(PropertyName = "Key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "Value")]
        public string Value { get; set; }
    }
    
}
