using SalesAnalisys.Data;
using SalesAnalisys.Models;
using SalesAnalisys.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace SalesAnalisys.Service.Test
{
    public class SalesAnalisysServiceTests
    {
      

        ISalesAnalisysService _salesAnalisysService;
        public SalesAnalisysServiceTests()
        {
            IDataFile dataFile = new DataFile();
            IModelTranslatorService modelTranslatorService = new ModelTranslatorService();
            _salesAnalisysService = new SalesAnalisysService(dataFile, modelTranslatorService);
        }

        [Fact(DisplayName = "ClientQuantityTests")]
        public void ClientQuantityTests()
        {
            //Arrange
            FileContent fileContent = new FileContent();
            fileContent.Clients.Add(new Client());
            fileContent.Clients.Add(new Client());
            fileContent.Clients.Add(new Client());
            fileContent.Clients.Add(new Client());

            //Act
            int clientQuantity = _salesAnalisysService.ClientQuantity(fileContent);

            //Assert
            Assert.Equal(4, clientQuantity);
        }
        [Fact(DisplayName = "SellerQuantityTests")]
        public void SellerQuantityTests()
        {
            //Arrange
            FileContent fileContent = new FileContent();
            fileContent.Sellers.Add(new Seller());
            fileContent.Sellers.Add(new Seller());

            //Act
            int clientQuantity = _salesAnalisysService.SellerQuantity(fileContent);

            //Assert
            Assert.Equal(2, clientQuantity);
        }
        [Fact(DisplayName = "MostExpensiveSaleTests")]
        public void MostExpensiveSaleTests()
        {
            //Arrange
            FileContent fileContent = new FileContent();
            fileContent.Sales.Add(new Sale
            {
                SaleID = 1,
                Items = new List<Item>
                {
                    new Item { ItemID = 1, Price = 7.50f, Quantity= 4},
                    new Item { ItemID = 2, Price = 2.50f, Quantity= 2}
                }
            });
            fileContent.Sales.Add(new Sale
            {
                SaleID = 2,
                Items = new List<Item>
                {
                    new Item { ItemID = 1, Price = 7.50f, Quantity= 5},
                    new Item { ItemID = 2, Price = 2.50f, Quantity= 5}
                }
            });
            fileContent.Sales.Add(new Sale
            {
                SaleID = 2,
                Items = new List<Item>
                {
                    new Item { ItemID = 3, Price = 10, Quantity= 2}
                }
            });
            //Act
            int? saleID = _salesAnalisysService.MostExpensiveSaleID(fileContent);

            //Assert
            Assert.Equal(2, saleID);
        }
        [Fact(DisplayName = "MostExpensiveSaleTests")]
        public void WorstSeller()
        {
            //Arrange
            FileContent fileContent = new FileContent();
            fileContent.Sales.Add(new Sale
            {
                SaleID = 1,
                Items = new List<Item>
                {
                    new Item { ItemID = 1, Price = 7.50f, Quantity= 4},
                    new Item { ItemID = 2, Price = 2.50f, Quantity= 2}
                },
                Seller = new Seller { Name = "Marco"}
            });
            fileContent.Sales.Add(new Sale
            {
                SaleID = 2,
                Items = new List<Item>
                {
                    new Item { ItemID = 1, Price = 7.50f, Quantity= 5},
                    new Item { ItemID = 2, Price = 2.50f, Quantity= 5}
                },
                Seller = new Seller { Name = "Marco" }
            });
            fileContent.Sales.Add(new Sale
            {
                SaleID = 2,
                Items = new List<Item>
                {
                    new Item { ItemID = 3, Price = 10, Quantity= 2}
                },
                Seller = new Seller { Name = "Carlos" }
            });
            //Act
            Seller seller = _salesAnalisysService.WorstSeller(fileContent);

            //Assert
            Assert.Equal("Carlos", seller.Name);
        }
    }
}
