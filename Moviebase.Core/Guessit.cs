using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Moviebase.Entities;
using Newtonsoft.Json;
using NLog;

namespace Moviebase.Core
{
    public class Guessit : IGuessit
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly Regex _imdbRegex;

        public Guessit()
        {
            _imdbRegex = new Regex("[0-9]{7}", RegexOptions.Compiled);
        }

        public GuessitResult RealGuessName(string filename)
        {
            return GuessImdbId(filename) ?? GuessName(filename);
        }

        public GuessitResult GuessName(string filename)
        {
            try
            {
                var vp = new ProcessStartInfo
                {
                    Arguments = "-j \"" + filename + "\"",
                    CreateNoWindow = true,
                    FileName = "guessit",
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };

                var proc = Process.Start(vp);
                Debug.Assert(proc != null);
                var output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
                return JsonConvert.DeserializeObject<GuessitResult>(output);
            }
            catch (Exception e)
            {
                _log.Error(e, "Error executing GuessIt.");
                return null;
            }
        }

        public GuessitResult GuessImdbId(string filename)
        {
            var matched = _imdbRegex.Match(filename);
            return matched.Success
                ? new GuessitResult {ImdbId = matched.Value}
                : null;
        }
    }
}
