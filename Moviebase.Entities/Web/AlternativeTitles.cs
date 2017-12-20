using System.Collections.Generic;
using Newtonsoft.Json;

namespace Moviebase.Entities.Web
{
    public class AlternativeTitles
    {
        [JsonProperty("titles")]
        public List<Title> Titles { get; set; }
    }
}
