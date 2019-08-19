using SalesAnalisys.Models;
using SalesAnalisys.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace SalesAnalisys.Service.Test
{
    public class SalesAnalisysServiceTests
    {
      
        FileContent _fileContent;
        SalesAnalisysService _salesAnalisysService;
        public SalesAnalisysServiceTests()
        {
            _fileContent = new FileContent();
            _salesAnalisysService = new SalesAnalisysService(_fileContent);
        }

        [Fact(DisplayName = "ClientQuantityTests")]
        public void ClientQuantityTests()
        {
            //Arrange
            _fileContent.Clients.Add(new Client());
            _fileContent.Clients.Add(new Client());
            _fileContent.Clients.Add(new Client());
            _fileContent.Clients.Add(new Client());

            //Act
            int clientQuantity = _salesAnalisysService.ClientQuantity();

            //Assert
            Assert.Equal(4, clientQuantity);
        }
        [Fact(DisplayName = "SellerQuantityTests")]
        public void SellerQuantityTests()
        {
            //Arrange
            _fileContent.Sellers.Add(new Seller());
            _fileContent.Sellers.Add(new Seller());

            //Act
            int clientQuantity = _salesAnalisysService.SellerQuantity();

            //Assert
            Assert.Equal(2, clientQuantity);
        }
        [Fact(DisplayName = "MostExpensiveSaleTests")]
        public void MostExpensiveSaleTests()
        {
            //Arrange
            _fileContent.Sales.Add(new Sale
            {
                SaleID = 1,
                Items = new List<Item>
                {
                    new Item { ItemID = 1, Price = 7.50f, Quantity= 4},
                    new Item { ItemID = 2, Price = 2.50f, Quantity= 2}
                }
            });
            _fileContent.Sales.Add(new Sale
            {
                SaleID = 2,
                Items = new List<Item>
                {
                    new Item { ItemID = 1, Price = 7.50f, Quantity= 5},
                    new Item { ItemID = 2, Price = 2.50f, Quantity= 5}
                }
            });
            _fileContent.Sales.Add(new Sale
            {
                SaleID = 2,
                Items = new List<Item>
                {
                    new Item { ItemID = 3, Price = 10, Quantity= 2}
                }
            });
            //Act
            int? saleID = _salesAnalisysService.MostExpensiveSaleID();

            //Assert
            Assert.Equal(2, saleID);
        }
        [Fact(DisplayName = "MostExpensiveSaleTests")]
        public void WorstSeller()
        {
            //Arrange
            _fileContent.Sales.Add(new Sale
            {
                SaleID = 1,
                Items = new List<Item>
                {
                    new Item { ItemID = 1, Price = 7.50f, Quantity= 4},
                    new Item { ItemID = 2, Price = 2.50f, Quantity= 2}
                },
                Seller = new Seller { Name = "Marco"}
            });
            _fileContent.Sales.Add(new Sale
            {
                SaleID = 2,
                Items = new List<Item>
                {
                    new Item { ItemID = 1, Price = 7.50f, Quantity= 5},
                    new Item { ItemID = 2, Price = 2.50f, Quantity= 5}
                },
                Seller = new Seller { Name = "Marco" }
            });
            _fileContent.Sales.Add(new Sale
            {
                SaleID = 2,
                Items = new List<Item>
                {
                    new Item { ItemID = 3, Price = 10, Quantity= 2}
                },
                Seller = new Seller { Name = "Carlos" }
            });
            //Act
            Seller seller = _salesAnalisysService.WorstSeller();

            //Assert
            Assert.Equal("Carlos", seller.Name);
        }
    }
}
