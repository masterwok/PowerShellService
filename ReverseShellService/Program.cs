using System.ServiceProcess;

namespace ReverseShellService
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ReverseShellService(args)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
