using System;

namespace Moviebase.Entities
{
    [Serializable]
    public class TmdbResult
    {
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string ImdbId { get; set; }
        public string Plot { get; set; }
        public string[] AlternativeNames { get; set; }
        public string PosterPath { get; set; }

        public override string ToString()
        {
            return $"{TmdbId}: {Title} ({Year})";
        }
    }
}
