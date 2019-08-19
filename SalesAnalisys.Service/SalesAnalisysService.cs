using SalesAnalisys.Models;
using System.Linq;
using SalesAnalisys.Utilities;

namespace SalesAnalisys.Services
{

    public interface ISalesAnalisysService
    {
        int ClientQuantity();
        int SellerQuantity();
        int? MostExpensiveSaleID();
        Seller WorstSeller();
    }
    public class SalesAnalisysService : ISalesAnalisysService
    {
        private FileContent _fileContent;
        public SalesAnalisysService(FileContent fileContent)
        {
            _fileContent = fileContent;
        }

        public int ClientQuantity() => _fileContent.Clients.Count;
        public int SellerQuantity() => _fileContent.Sellers.Count;

        public int? MostExpensiveSaleID()
        {
            if (_fileContent.Sales.Count == 0)
                return null;

            return _fileContent.Sales
                .Select(s => new { s.SaleID, Total = s.TotalSale() })
                .OrderByDescending(s => s.Total).First().SaleID;

        }

        public Seller WorstSeller()
        {

            if (_fileContent.Sales.Count == 0)
                return null;

            return _fileContent.Sales
                 .Select(s => new { s.Seller, Total = s.TotalSale() })
                 .OrderBy(s => s.Total).First().Seller;

        }

    }
}
