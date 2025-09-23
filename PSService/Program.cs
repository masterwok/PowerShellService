using System.ServiceProcess;

namespace PSService
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new PSService(args)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
