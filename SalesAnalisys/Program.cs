
using Microsoft.Extensions.DependencyInjection;
using SalesAnalisys.Data;
using SalesAnalisys.Models;
using SalesAnalisys.Services;
using System;

namespace SalesAnalisys
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            RegisterServices();
            var salesAnalisysService = _serviceProvider.GetService<ISalesAnalisysService>();

            string analisysContent = salesAnalisysService.SalesAnalisys();
            Console.WriteLine(analisysContent);
            new System.Threading.AutoResetEvent(false).WaitOne();

            DisposeServices();
        }
        private static void RegisterServices()
        {
            var collection = new ServiceCollection();

            collection.AddScoped<IDataFile, DataFile>();
            collection.AddScoped<IModelTranslatorService, ModelTranslatorService>();
            collection.AddScoped<ISalesAnalisysService, SalesAnalisysService>();
            _serviceProvider = collection.BuildServiceProvider();
        }


        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
