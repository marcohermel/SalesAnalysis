using Microsoft.Extensions.FileProviders;

namespace SalesAnalisys.Utilities
{
   public static class FileInfoExtensions
    {
        public static byte[] ReadBytes(this IFileInfo fileInfo)
        {
            var stream = fileInfo.CreateReadStream();
            byte[] bytes = new byte[stream.Length + 10];
            int numBytesToRead = (int)stream.Length;
            int numBytesRead = 0;
            do
            {
                int n = stream.Read(bytes, numBytesRead, 10);
                numBytesRead += n;
                numBytesToRead -= n;
            } while (numBytesToRead > 0);
            stream.Close();

            return bytes;
        }
    }
}
