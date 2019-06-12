using System;
using System.Collections.Generic;

namespace AlbionPriceChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(
                    "HOW TO USE:\n" +
                    "----------------------------------\n" +
                    "1. Copy chest logs and put them to the chestlog.txt file in the application folder\n" +
                    "2. Run the .exe and type in the preffered city (Not all prices are from this city though due to inaccuarcy in some)\n" +
                    "3. After processing you can see the data in excel file under exported data folder.\n" +
                    "If you don't have excel, then there are some online excel viewers.\n" +
                    "----------------------------------\n"
                );

                Console.WriteLine("Type the city you want to prioritize");
                var city = Console.ReadLine().ToUpper();

                Console.WriteLine("Parsing chest data..");
                var chestItems = ChestLogHandler.GetChestData();

                Console.WriteLine("Parsing metadata..");
                var itemsWithMetaData = MetaDataHandler.GetMetaNames(chestItems);

                Console.WriteLine("Requesting information from API");
                var itemsWithPrice = ApiHandler.GetApiPrices(itemsWithMetaData);

                Console.WriteLine("Calculating prices..");
                var itemsWithAccuratePrice =
                    Calculations.CalculateAccuratePriceFromMultipleCities(itemsWithPrice, city);

                foreach (var item in itemsWithPrice)
                {
                    if (item.MetaName != null)
                    {
                        Console.WriteLine(
                            $"Item: {item.Name} | Price: {item.MostAccuratePrice} | City: {item.MostAccurateCity.Name} |  Amount: {item.Amount}");
                    }
                    else
                    {
                        Console.WriteLine("Did not find metaname for: " + item.Name);
                    }
                }

                Console.WriteLine("Total value: " + Calculations.CalculateTotalValue(itemsWithAccuratePrice));
                Console.WriteLine("Exporting data to excel..");
                Console.WriteLine("Data saved to: " + SaveData.SaveCityInfo(city, itemsWithPrice));
                Console.WriteLine("Completed! Press any key to exit");
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            Console.ReadKey();
        }
    }
}
