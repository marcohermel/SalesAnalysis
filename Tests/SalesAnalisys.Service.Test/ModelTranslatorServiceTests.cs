using SalesAnalisys.Data;
using SalesAnalisys.Models;
using SalesAnalisys.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SalesAnalisys.Service.Test
{
    public class ModelTranslatorServiceTests
    {
        ModelTranslatorService _modelTranslatorService;
        public ModelTranslatorServiceTests()
        {

            _modelTranslatorService = new ModelTranslatorService();
        }

        [Fact(DisplayName = "TranslateToFileContent")]
        public void TranslateToFileContent()
        {
            //Arrange
            string allfilesContent = "001ç1234567891234çPedroç50000 \n" +
                "001ç3245678865434çPauloç40000.99 \n" +
                "002ç2345675434544345çJose da SilvaçRural \n" +
                 " 002ç2345675433444345çEduardo PereiraçRural \n" +
                 "003ç10ç[1-10-100,2-30-2.50,3-40-3.10]çPedro \n" +
                 " 003ç08ç[1-34-10,2-33-1.50,3-40-0.10]çPaulo \n";

            //Act
            var translated = _modelTranslatorService.TranslateToFileContent(allfilesContent);

            //Assert
            Assert.IsType<FileContent>(translated);
        }

        [Fact(DisplayName = "TranslateToSeller")]
        public void TranslateToSeller()
        {
            //Arrange
            string[] content = "001ç1234567891234çPedroç50000".Split("ç");

            //Act
            var translated = _modelTranslatorService.TranslateToSeller(content);

            //Assert
            Assert.IsType<Seller>(translated);
            Assert.Equal("1234567891234", translated.CPF);
            Assert.Equal("Pedro", translated.Name);
            Assert.Equal(50000f, translated.Salary);
        }

        [Fact(DisplayName = "TranslateToClient")]
        public void TranslateToClient()
        {
            //Arrange
            string[] content = "002ç2345675434544345çJose da SilvaçRural".Split("ç");

            //Act
            var translated = _modelTranslatorService.TranslateToClient(content);

            //Assert
            Assert.IsType<Client>(translated);
            Assert.Equal("2345675434544345", translated.CNPJ);
            Assert.Equal("Jose da Silva", translated.Name);
            Assert.Equal("Rural", translated.Area);
        }


        [Fact(DisplayName = "SplitLines")]
        public void SplitLines()
        {
            //Arrange
            string content = "001ç1234567891234çPedroç50000\n003ç10ç[1-10-100,2-30-2.50,3-40-3.10]çPedro";

            //Act
            string[] lines = _modelTranslatorService.SplitLines(content);

            //Assert    
            Assert.Equal(2, lines.Length);
        }
        [Fact(DisplayName = "TranslateLine")]
        public void TranslateLine()
        {
            //Arrange
            FileContent fileContent = new FileContent();
            string content = "002ç2345675434544345çJose da SilvaçRural";

            //Act
            _modelTranslatorService.TranslateLine(content, ref fileContent);

            //Assert    
            Assert.Equal("Jose da Silva", fileContent.Clients.FirstOrDefault().Name);
        }

        [Fact(DisplayName = "TranslateToSale")]
        public void TranslateToSale()
        {
            //Arrange
            List<Seller> sellers = new List<Seller> { new Seller { Name = "Pedro" } };
            string[] content = "003ç10ç[1-10-100,2-30-2.50,3-40-3.10]çPedro".Split("ç");


            //Act
            var translated = _modelTranslatorService.TranslateToSale(content, sellers);

            //Assert
            Assert.IsType<Sale>(translated);
            Assert.Equal(10, translated.SaleID);
            Assert.Collection(translated.Items,
                first => Assert.IsType<Item>(first),
                second => Assert.IsType<Item>(second),
                third => Assert.IsType<Item>(third)
             );
            Assert.Equal("Pedro", translated.Seller.Name);
        }

        [Fact(DisplayName = "TranslateToItens")]
        public void TranslateToItens()
        {
            //Arrange
            string content = "1-34-10,2-33-1.50,3-40-0.10";

            //Act
            var translated = _modelTranslatorService.TranslateToItens(content);

            //Assert
            Assert.Collection(translated,
                one => { Assert.Equal(1, one.ItemID); Assert.Equal(34, one.Quantity); Assert.Equal(10, one.Price); },
                two => { Assert.Equal(2, two.ItemID); Assert.Equal(33, two.Quantity); Assert.Equal(1.50f, two.Price); },
                three => { Assert.Equal(3, three.ItemID); Assert.Equal(40, three.Quantity); Assert.Equal(0.10f, three.Price); }
            );
        }

        [Fact(DisplayName = "TranslateToItem")]
        public void TranslateToItem()
        {
            //Arrange
            string[] content = "1-34-10".Split("-");

            //Act
            var translated = _modelTranslatorService.TranslateToItem(content);

            //Assert
            Assert.IsType<Item>(translated);
            Assert.Equal(1, translated.ItemID);
            Assert.Equal(34, translated.Quantity);
            Assert.Equal(10, translated.Price);
        }
    }
}
