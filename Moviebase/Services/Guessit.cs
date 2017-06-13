using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Moviebase.Domain;
using Newtonsoft.Json;

namespace Moviebase.Services
{
    public class Guessit : IGuessit
    {
        private const string ApiEndpoint = "https://v2.api.guessit.io/?filename=";
        private readonly Regex _imdbRegex;

        public Guessit()
        {
            _imdbRegex = new Regex("tt[0-9]{7}", RegexOptions.Compiled);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }
        
        public string GuessImdbId(string filename)
        {
            var matched = _imdbRegex.Match(filename);
            return matched.Success ? matched.Value : null;
        }

        public GuessitResult GuessName(string filename)
        {
            var uri = ApiEndpoint + Uri.EscapeUriString(filename);
            return HttpHelper.GetRequestBody<GuessitResult>(uri);
        }
    }
}
