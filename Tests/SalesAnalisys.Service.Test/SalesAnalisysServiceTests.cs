using SalesAnalisys.Data;
using SalesAnalisys.Models;
using SalesAnalisys.Services;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;

namespace SalesAnalisys.Service.Test
{
    public class SalesAnalisysServiceTests
    {
        Mock<IModelTranslatorService> _modelTranslatorService;
        Mock<IDataFile> _dataFile;
        ISalesAnalisysService _salesAnalisysService;
        public SalesAnalisysServiceTests()
        {
            _modelTranslatorService = new Mock<IModelTranslatorService>();
            _dataFile = new Mock<IDataFile>();
            _salesAnalisysService = new SalesAnalisysService(_dataFile.Object, _modelTranslatorService.Object);
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
                Seller = new Seller { Name = "Marco" }
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

        [Fact(DisplayName = "SalesAnalisysTest")]
        public void SalesAnalisysTest()
        {

            //Arrange
            string content = "001Á123456789ÁMarcoÁRural \n 001Á987654321ÁFelipeÁRural";

            FileContent fileContent = new FileContent
            {
                Clients = new List<Client> {
                new Client{ CNPJ="123456789", Name= "Marco", Area = "Rural"},
                new Client{ CNPJ="987654321", Name= "Felipe", Area = "Rural"}
                }
            };
            _dataFile.Setup(m => m.ReadAllFiles(It.IsAny<string>())).Returns(content);
            _dataFile.Setup(m => m.WriteFile(It.IsAny<string>(), It.IsAny<string>())).Verifiable();


            _modelTranslatorService.Setup(m => m.TranslateToFileContent(It.IsAny<string>())).Returns(fileContent);

            //Act
            string analisysContent = _salesAnalisysService.SalesAnalisys();

            //Assert
            Assert.Contains("Quantidade de clientes no arquivo de entrada:2", analisysContent);
            _dataFile.Verify(x => x.WriteFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
