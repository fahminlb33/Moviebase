using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Moviebase.Core
{
    public class ComponentManager : IComponentManager
    {
        public bool CheckPythonInstallation()
        {
            var vp = new ProcessStartInfo
            {
                Arguments = "-V",
                CreateNoWindow = true,
                FileName = "python",
                RedirectStandardError = true,
                UseShellExecute = false
            };

            var proc = Process.Start(vp);
            Debug.Assert(proc != null);
            var output = proc.StandardError.ReadToEnd();
            proc.WaitForExit();
            return output.Contains("Python");
        }

        public bool CheckGuessItInstallation()
        {
            var vp = new ProcessStartInfo
            {
                Arguments = "--version",
                CreateNoWindow = true,
                FileName = "guessit",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var proc = Process.Start(vp);
            Debug.Assert(proc != null);
            var output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
            return output.Contains("GuessIt");
        }
    }
}
