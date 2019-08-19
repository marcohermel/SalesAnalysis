using SalesAnalisys.Models;

namespace SalesAnalisys.Utilities
{
    public static class SaleExtensions
    {
        public static float TotalSale(this Sale sale)
        {
            float totalSale = 0;
            foreach (var item in sale.Items)
                totalSale += (item.Price * item.Quantity);
            return totalSale;
        }
    }
}
