using System.Collections.Generic;

namespace SalesAnalisys.Models
{
    public class Sale
    {
        public int SaleID { get; set; }
        public List<Item> Items { get; set; }
        public Seller Seller { get; set; }
    }

    public class Item
    {
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
