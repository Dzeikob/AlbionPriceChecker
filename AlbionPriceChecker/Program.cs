using System;

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
                    "2. Run the .exe and type in the city\n" +
                    "3. After processing you can see the data in excel file under exported data folder.\n" +
                    "If you don't have excel, then there are some online excel viewers.\n" +
                    "----------------------------------\n"
                );

                Console.WriteLine("Type the city you want to get prices from:");
                var city = Console.ReadLine().ToUpper();

                Console.WriteLine("Parsing chest data..");
                var chestItems = ChestLogHandler.GetChestData();

                Console.WriteLine("Parsing metadata..");
                var itemsWithMetaData = MetaDataHandler.GetMetaNames(chestItems);

                Console.WriteLine("Requesting information from API");
                var itemsWithPrice = ApiHandler.GetApiPrices(itemsWithMetaData, city);

                Console.WriteLine("Calculating prices..");
                var totalValue = Calculations.CalculateTotalValue(itemsWithPrice);

                foreach (var item in itemsWithPrice)
                {
                    Console.WriteLine($"Item: {item.Name} | Price: {item.SellPriceMin} |  Amount: {item.Amount}");
                }
                Console.WriteLine("Total value: " + totalValue + " (Should not be trusted due to possible inaccurate data)");

                Console.WriteLine("Exporting data to excel..");
                Console.WriteLine("Data saved to: " + SaveData.SaveCityInfo(city, itemsWithPrice));
                Console.WriteLine("Completed! Press any key to exit");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
