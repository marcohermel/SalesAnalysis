using System;
using System.Collections.Generic;
using System.Text;

namespace SalesAnalisys.Models
{
    public class FileContent
    {
        public List<Client> Clients { get; set; }
        public List<Seller> Sellers { get; set; }
        public List<Sale> Sales { get; set; }

        public FileContent()
        {
            Clients = new List<Client>();
            Sellers = new List<Seller>();
            Sales = new List<Sale>();
        }
    }
}
