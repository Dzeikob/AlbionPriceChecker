using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Linq;

namespace AlbionPriceChecker
{
    public static class ChestLogHandler
    {
        public static List<Item> GetChestData()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"chestlog.txt");
            string[] chestLog = System.IO.File.ReadAllLines(path);
            List<Item> chestItems = new List<Item>();
            

            foreach (string line in chestLog)
            {
                string[] regexMatch = Regex.Split(line, "\"(.*?)\"");
                if (line.Trim() == "") continue;
                var existingItem = chestItems.Find(x =>
                    x.Name == regexMatch[5] && x.Enchantment == Convert.ToInt16(regexMatch[7]));
                if (existingItem != null)
                {
                    existingItem.Amount += Convert.ToInt16(regexMatch[11]);
                }
                else
                {
                    chestItems.Add(new Item()
                    {
                        Name = regexMatch[5],
                        Enchantment = Convert.ToInt16(regexMatch[7]),
                        Quality = Convert.ToInt16(regexMatch[9]),
                        Amount = Convert.ToInt16(regexMatch[11])
                    });
                }
            }

            return chestItems;
        }
    }
}
