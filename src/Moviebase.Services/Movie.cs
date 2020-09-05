using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Moviebase.Services
{
    public class Movie
    {
        [JsonProperty("id")]
        public int TmdbId { get; set; }
        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }

        [JsonProperty("release_date")]
        public DateTimeOffset ReleaseDate { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("tagline")]
        public string Tagline { get; set; }
        [JsonProperty("overview")]
        public string Overview { get; set; }
        [JsonProperty("genres")]
        public List<Genre> Genre { get; set; }
        [JsonProperty("vote_average")]
        public float VoteAverage { get; set; }
        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
    }
}
