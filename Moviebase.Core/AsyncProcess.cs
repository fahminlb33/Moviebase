using System.Diagnostics;
using System.Threading.Tasks;

namespace Moviebase.Core
{
    public class AsyncProcess
    {
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

            switch (stream)
            {
                case ProcessRedirectStream.StandardError:
                    process.StartInfo.RedirectStandardError = true;
                    break;
                case ProcessRedirectStream.StandardOuput:
                    process.StartInfo.RedirectStandardOutput = true;
                    break;
            }

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
    }
}
