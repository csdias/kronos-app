using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            using (Service service = new Service())
            {
                service.StartService();
                Console.WriteLine("Press any key to exit...");
                Thread.Sleep(Timeout.Infinite);
                service.Stop();
            }

            Environment.Exit(0);
#else
            ServiceBase.Run(new ServiceBase[] {new Service()});
#endif
        }
    }
}
