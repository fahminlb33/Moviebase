using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        public async Task<GuessitResult> RealGuessName(string filename)
        {
            return await GuessImdbId(filename) ?? await GuessName(filename);
        }

        public async Task<GuessitResult> GuessName(string filename)
        {
            try
            {
                var output = await AsyncProcess.StartWithOuput("guessit", "-j \"" + filename + "\"", ProcessRedirectStream.StandardOuput);
                return JsonConvert.DeserializeObject<GuessitResult>(output);
            }
            catch (Exception e)
            {
                _log.Error(e, "Error executing GuessIt.");
                return null;
            }
        }

        public async Task<GuessitResult> GuessImdbId(string filename)
        {
            await Task.Yield(); // TODO: is this the right usage of Task.Yield()?
            var matched = _imdbRegex.Match(filename);
            return matched.Success
                ? new GuessitResult {ImdbId = matched.Value}
                : null;
        }
    }
}
