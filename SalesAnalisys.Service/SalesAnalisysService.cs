using SalesAnalisys.Models;
using System.Linq;
using SalesAnalisys.Utilities;
using SalesAnalisys.Data;
using System.Text;
using System;

namespace SalesAnalisys.Services
{

    public interface ISalesAnalisysService
    {
        FileContent LoadFiles();
        void SalesAnalisys(FileContent fileContent);
        int ClientQuantity(FileContent fileContent);
        int SellerQuantity(FileContent fileContent);
        int? MostExpensiveSaleID(FileContent fileContent);
        Seller WorstSeller(FileContent fileContent);
    }
    public class SalesAnalisysService : ISalesAnalisysService
    {
        IDataFile _dataFile;
        IModelTranslatorService _modelTranslatorService;
      

        public SalesAnalisysService(IDataFile dataFile, IModelTranslatorService modelTranslatorService)
        {
            _dataFile = dataFile;
            _modelTranslatorService = modelTranslatorService;
          
        }
        public FileContent LoadFiles()
        {
            _dataFile.CreateDirectory(DataPath.InputPath);
            _dataFile.CreateDirectory(DataPath.OutputPath);
            var content = _dataFile.ReadAllFiles(DataPath.InputPath);
            return _modelTranslatorService.TranslateToFileContent(content);
        }
        public void SalesAnalisys(FileContent fileContent)
        {
            LoadFiles();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Quantidade de clientes no arquivo de entrada:{ClientQuantity(fileContent)}");
            sb.AppendLine($"Quantidade de vendedores no arquivo de entrada:{SellerQuantity(fileContent)}");
            sb.AppendLine($"ID da venda mais cara:{MostExpensiveSaleID(fileContent)}");
            sb.AppendLine($"O pior vendedor:{WorstSeller(fileContent)?.Name}");

            string fileName = "OutputFile.txt";
            _dataFile.WriteFile($"{DataPath.OutputPath}\\{fileName}", sb.ToString());

            Console.WriteLine(sb.ToString());
            new System.Threading.AutoResetEvent(false).WaitOne();
        }

        public int ClientQuantity(FileContent fileContent) => fileContent.Clients.Count;
        public int SellerQuantity(FileContent fileContent) => fileContent.Sellers.Count;

        public int? MostExpensiveSaleID(FileContent fileContent)
        {
            if (fileContent.Sales.Count == 0)
                return null;

            return fileContent.Sales
                .Select(s => new { s.SaleID, Total = s.TotalSale() })
                .OrderByDescending(s => s.Total).First().SaleID;

        }

        public Seller WorstSeller(FileContent fileContent)
        {

            if (fileContent.Sales.Count == 0)
                return null;

            return fileContent.Sales
                 .Select(s => new { s.Seller, Total = s.TotalSale() })
                 .OrderBy(s => s.Total).First().Seller;

        }
    }
}
