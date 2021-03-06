﻿using Newtonsoft.Json;

namespace Moviebase.Entities
{
    public class GuessitResult
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("year")]
        public int Year { get; set; }
        [JsonProperty("imdbId")]
        public string ImdbId { get; set; }
        [JsonProperty("screen_size")]
        public string ScreenSize { get; set; }
        [JsonProperty("release_group")]
        public string ReleaseGroup { get; set; }
    }
}
