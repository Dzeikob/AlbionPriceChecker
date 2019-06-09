using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace AlbionPriceChecker
{
    public static class MetaDataHandler
    {
        public static List<Item> GetMetaNames(List<Item> chestItems)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"items.json");
            List<MetaItem> metaItems;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                metaItems = JsonConvert.DeserializeObject<List<MetaItem>>(json);
            }

            foreach (var item in chestItems)
            {
                var metaItem = metaItems.Find(x => x.LocalizedNames != null && x.LocalizedNames[0].Value == item.Name);
                //Remove @ITEMS_ from localizationNameVariable
                item.MetaName = metaItem.LocalizationNameVariable.Substring(7);
                if (item.Enchantment > 0) item.MetaName += "@" + item.Enchantment;
            }

            return chestItems;
        }
    }
}
