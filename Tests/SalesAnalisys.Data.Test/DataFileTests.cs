using System;
using System.IO;
using Xunit;

namespace SalesAnalisys.Data.Test
{
    public class DataFileTests
    {
        DataFile _dataFile;
        public DataFileTests()
        {
            _dataFile = new DataFile();
            Directory.CreateDirectory(DataPath.InputPath);
        }

        [Fact(DisplayName = "ReadAllFiles")]
        public  void ReadAllFiles()
        {
            //Arrange
            string folderPath = DataPath.InputPath;

            //Act
            string fileContent =  _dataFile.ReadAllFiles(folderPath);

            //Assert
            Assert.Contains("001", fileContent);
        }

        [Fact(DisplayName = "ReadFile")]
        public  void ReadFile()
        {
            //Arrange
            string filePath = $"{DataPath.InputPath}/file.txt";
            _dataFile.WriteFile(filePath, "001ç1234567891234çPedroç50000");

            //Act
            string fileContent =  _dataFile.ReadFile(filePath);

            //Assert
            Assert.Contains("001", fileContent);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        [Fact(DisplayName = "WriteFile")]
        public void WriteFile()
        {
            //Arrange
            string content = "001ç1234567891234çPedroç50000";
            string fileName = "TestFile.txt";
            string filePath = $"{DataPath.OutputPath}\\{fileName}";
            if (File.Exists(filePath))
                File.Delete(filePath);
            //Act
            _dataFile.WriteFile(filePath, content);

            //Assert
           Assert.True(File.Exists(filePath));
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
