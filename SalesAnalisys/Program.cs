
using SalesAnalisys.Services;

namespace SalesAnalisys
{
    class Program
    {

   
        static void Main(string[] args)
        {
            WatcherService watcherService = new WatcherService();
            watcherService.WatchDirectory();
            watcherService.SalesAnalisys();

        }

      
    }
}
