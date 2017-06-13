using System.IO;
using System.Text;
using Moviebase.Domain;

namespace Moviebase.Services
{
    public class Renamer
    {
        public static string GetReplacedPattern(string pattern, MovieEntry entry)
        {
            var sb = new StringBuilder();
            sb.Append(pattern);

            // replace
            sb.Replace("{Title}", entry.Title);
            sb.Replace("{Imdb}", entry.ImdbId);
            sb.Replace("{Year}", entry.Year.ToString());
            sb.Replace("{Extension}", Path.GetExtension(entry.FileName));
         
            return sb.ToString();
        }
    }
}
