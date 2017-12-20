using System;

namespace Moviebase.Entities
{
    [Serializable]
    public class TmdbResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string ImdbId { get; set; }
        public string Plot { get; set; }
        public string[] AlternativeNames { get; set; }
        public string PosterPath { get; set; }
    }
}
