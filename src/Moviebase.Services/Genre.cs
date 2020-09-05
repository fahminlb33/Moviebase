using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moviebase.Services
{
    public class Genre
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
