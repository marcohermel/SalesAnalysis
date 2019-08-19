using SalesAnalisys.Data;
using SalesAnalisys.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SalesAnalisys.Services
{

    public class WatcherService
    {
        private FileSystemWatcher _watcher;

        public WatcherService()
        {
            _watcher = new FileSystemWatcher();
        }
        public void WatchDirectory()
        {
            _watcher.Path = DataPath.InputPath;
            _watcher.IncludeSubdirectories = true;
            _watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                         | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _watcher.Filter = "*.*";
            _watcher.Changed += new FileSystemEventHandler (OnChanged);
            _watcher.Created += new FileSystemEventHandler(OnChanged);
            _watcher.Deleted += new FileSystemEventHandler(OnChanged);
            _watcher.Renamed += new RenamedEventHandler(OnChanged);
            _watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            SalesAnalisys();
        }

        public void SalesAnalisys()
        {
            DataFile dataFile = new DataFile();
            dataFile.CreateDirectory(DataPath.InputPath);
            ModelTranslatorService modelTranslatorService = new ModelTranslatorService();
            string content = dataFile.ReadAllFiles(DataPath.InputPath);
            FileContent fileContent = modelTranslatorService.TranslateToFileContent(content);
            SalesAnalisysService salesAnalisysService = new SalesAnalisysService(fileContent);

            int clientQuantity = salesAnalisysService.ClientQuantity();
            int sellerQuantity = salesAnalisysService.SellerQuantity();
            int? mostExpensiveSaleID = salesAnalisysService.MostExpensiveSaleID();
            Seller WorstSeller = salesAnalisysService.WorstSeller();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Quantidade de clientes no arquivo de entrada:{clientQuantity}");
            sb.AppendLine($"Quantidade de vendedores no arquivo de entrada:{sellerQuantity}");
            sb.AppendLine($"ID da venda mais cara:{mostExpensiveSaleID}");
            sb.AppendLine($"O pior vendedor:{WorstSeller?.Name}");

            string fileName = "OutFile.txt";
            string filePath = $"{DataPath.OutputPath}\\{fileName}";
            dataFile.WriteFile(filePath, sb.ToString());

            Console.WriteLine(sb.ToString());
           // _watcher.BeginInit();
            new System.Threading.AutoResetEvent(false).WaitOne();
        }
    }
}
