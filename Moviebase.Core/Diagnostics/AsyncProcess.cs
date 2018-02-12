using System.Diagnostics;
using System.Threading.Tasks;

namespace Moviebase.Core.Diagnostics
{
    public class AsyncProcess
    {
        [DebuggerStepThrough]
        public static Task<string> StartWithOuput(string filePath, string arguments, ProcessRedirectStream stream)
        {
            var tcs = new TaskCompletionSource<string>();
            var process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo =
                {
                    Arguments = arguments,
                    CreateNoWindow = true,
                    FileName = filePath,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false
                }
            };
            SelectRedirectStream(ref process, stream);
            process.Exited += async (sender, args) =>
            {
                switch (stream)
                {
                    case ProcessRedirectStream.StandardError:
                        tcs.SetResult(await process.StandardError.ReadToEndAsync());
                        break;
                    case ProcessRedirectStream.StandardOuput:
                        tcs.SetResult(await process.StandardOutput.ReadToEndAsync());
                        break;
                }
            };

            process.Start();
            return tcs.Task;
        }

        private static void SelectRedirectStream(ref Process process, ProcessRedirectStream stream)
        {
            switch (stream)
            {
                case ProcessRedirectStream.StandardError:
                    process.StartInfo.RedirectStandardError = true;
                    break;
                case ProcessRedirectStream.StandardOuput:
                    process.StartInfo.RedirectStandardOutput = true;
                    break;
            }
        }
    }
}
