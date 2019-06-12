using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using ClosedXML;
using ClosedXML.Excel;

namespace AlbionPriceChecker
{
    public static class SaveData
    {
        public static string SaveCityInfo(string city, List<Item> itemsWithPrices)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "exported data");
            Directory.CreateDirectory(path);

            var table = GetDataTable();
            foreach (var item in itemsWithPrices)
            {
                if (item.MetaName != null)
                {
                    table.Rows.Add(item.Name, item.Enchantment, item.MostAccuratePrice, item.MostAccurateCity.Name, item.Amount, item.MostAccuratePrice * item.Amount);
                }
                else
                {
                    table.Rows.Add(item.Name, item.Enchantment, 0, "N/A", item.Amount, 0);
                }
            }

            var summaryTable = GetSummaryTable();
            summaryTable.Rows.Add(Calculations.CalculateTotalValue(itemsWithPrices));

            string time = DateTime.Now.ToString("yyyyMMdd_HHmm");
            XLWorkbook workbook = new XLWorkbook();
            workbook.Worksheets.Add(table);
            workbook.Worksheets.Add(summaryTable);
            workbook.SaveAs(Path.Combine(path, $"{city}_{time}.xlsx"));
            return Path.Combine(path, $"{city}_{time}.xlsx");
        }

        private static DataTable GetDataTable()
        {
            DataTable table = new DataTable();
            table.TableName = "Prices";
            table.Columns.Add("Item Name", typeof(string));
            table.Columns.Add("Enchantment", typeof(int));
            table.Columns.Add("Price per item", typeof(long));
            table.Columns.Add("City", typeof(string));
            table.Columns.Add("Quanity", typeof(int));
            table.Columns.Add("Total price", typeof(long));
            
            return table;
        }

        private static DataTable GetSummaryTable()
        {
            DataTable table = new DataTable();
            table.TableName = "Summary";
            table.Columns.Add("Total cost", typeof(string));
            return table;
        }
    }
}
