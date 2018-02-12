using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Moviebase.Core.Diagnostics;
using Moviebase.Entities;
using Newtonsoft.Json;

namespace Moviebase.Core.Services
{
    public class Guessit : IGuessit
    {
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
                var output = await AsyncProcess.StartWithOuput("guessit", $"-j \"{filename}\"", ProcessRedirectStream.StandardOuput);
                return JsonConvert.DeserializeObject<GuessitResult>(output);
            }
            catch (Exception e)
            {
                Debug.Print("GuessIt error: " + e.Message);
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
