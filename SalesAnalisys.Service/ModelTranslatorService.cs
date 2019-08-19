using System;
using System.Collections.Generic;
using System.Linq;
using SalesAnalisys.Models;
using SalesAnalisys.Utilities;

namespace SalesAnalisys.Services
{
    public class ModelTranslatorService
    {

        public FileContent TranslateToFileContent(string AllfilesContent)
        {

            FileContent fileContent = new FileContent();

            var lines = SplitLines(AllfilesContent);

            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line.Trim()))
                    TranslateLine(line.Trim(), ref fileContent);
            }

            return fileContent;
        }

        public void TranslateLine(string line, ref FileContent fileContent)
        {
            var lineValues = SplitLineValues(line);

            switch ((LineType)lineValues[0].ToInt())
            {
                case LineType.Seller:
                    fileContent.Sellers.Add(TranslateToSeller(lineValues));
                    break;

                case LineType.Client:
                    fileContent.Clients.Add(TranslateToClient(lineValues));
                    break;

                case LineType.Sales:
                    fileContent.Sales.Add(TranslateToSale(lineValues, fileContent.Sellers));
                    break;
                default:
                    break;
            }
        }

        public Seller TranslateToSeller(string[] line) => new Seller { CPF = line[1], Name = line[2], Salary = line[3].ToFloat() };
        public Client TranslateToClient(string[] line) => new Client { CNPJ = line[1], Name = line[2], Area = line[3] };
        public Sale TranslateToSale(string[] line, List<Seller> sellers)
        {
            return new Sale
            {
                SaleID = line[1].ToInt(),
                Items = TranslateToItens(line[2]).ToList(),
                Seller = getSellerByName(line[3], sellers)
            };
        }

        public IEnumerable<Item> TranslateToItens(string itens)
        {
            var arrayItens = SplitItens(itens.Replace("[", "").Replace("]", ""));

            foreach (var item in arrayItens)
            {
                var itemValues = SplitItemValues(item);
                yield return TranslateToItem(itemValues);
            }
        }

        public Item TranslateToItem(string[] item) => new Item { ItemID = item[0].ToInt(), Quantity = item[1].ToInt(), Price = item[2].ToFloat() };
        private Seller getSellerByName(string sellerName, List<Seller> sellers) => sellers?.FirstOrDefault(s => s.Name == sellerName);
        public string[] SplitLines(string fileContent)
        {

            string[] stringSeparators = new string[] { "\n" };
            return fileContent.Split(stringSeparators, StringSplitOptions.None);
        }
        private string[] SplitLineValues(string line) => line.Split('ç');
        private string[] SplitItens(string itens) => itens.Split(',');
        private string[] SplitItemValues(string item) => item.Split('-');

    }
}
