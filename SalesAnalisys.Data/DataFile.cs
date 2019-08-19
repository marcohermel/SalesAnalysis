using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Text;


namespace SalesAnalisys.Data
{
    public class DataFile
    {

        public void CreateDirectory(string folderPath) => Directory.CreateDirectory(folderPath);

        public string ReadAllFiles(string folderPath)
        {
            PhysicalFileProvider physicalFileProvider = new PhysicalFileProvider(folderPath);
            IDirectoryContents directoryContents = physicalFileProvider.GetDirectoryContents(string.Empty);

            StringBuilder filesContent = new StringBuilder();
            foreach (var fileInfo in directoryContents)
                filesContent.Append("\n" +  ReadFile(fileInfo.PhysicalPath));

            return filesContent.ToString();
        }


        public string ReadFile(string filePath)
        {
            byte[] result;
            using (FileStream stream = File.Open(filePath, FileMode.Open))
            {
                result = new byte[stream.Length];
                stream.Read(result, 0, (int)stream.Length);
            }

            return Encoding.GetEncoding("iso-8859-1").GetString(result, 0, result.Length);
        }
        public void WriteFile(string filePath, string content) => File.WriteAllText(filePath, content);
  

    }
}
