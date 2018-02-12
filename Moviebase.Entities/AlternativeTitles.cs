using System.Collections.Generic;
using Newtonsoft.Json;

namespace Moviebase.Entities
{
    public class AlternativeTitles
    {
        [JsonProperty("titles")]
        public List<Title> Titles { get; set; }
    }
}
