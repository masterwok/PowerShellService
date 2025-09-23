using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace PSService
{
    public partial class PSService : ServiceBase
    {
        private readonly string[] autoArgs;
        private Thread thread;

        public PSService(string[] autoArgs)
        {
            InitializeComponent();

            this.autoArgs = autoArgs;
        }

        protected override void OnStart(string[] _)
        {
            var encodedCommand = (autoArgs.Length > 0 && !string.IsNullOrEmpty(autoArgs[0]))
                ? autoArgs[0]
                : null;

            if(string.IsNullOrEmpty(encodedCommand))
            {
                Stop();
                return;
            }

            thread = new Thread(() => RunEncodedPowerShell(encodedCommand))
            {
                IsBackground = true
            };

            thread.Start();
        }

        private void RunEncodedPowerShell(string encoded)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-nop -w hidden -enc {encoded}",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var proc = Process.Start(psi)) { proc.WaitForExit(); }
        }

        protected override void OnStop()
        {
            if(thread.IsAlive)
            {
                thread.Abort();
            }
        }
    }
}
