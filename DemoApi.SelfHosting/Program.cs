using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.SelfHosting
{
    using Microsoft.Owin.Hosting;

    class Program
    {
        static void Main(string[] args)
        {
            IDisposable app = WebApp.Start<Startup>("http://localhost:42424");
            Console.WriteLine("API Server is running.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            app.Dispose();
        }
    }
}
