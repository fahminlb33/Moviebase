using System;
using System.IO;

namespace Moviebase.Domain
{
    [Serializable]
    public class MovieEntry
    {
        // ----- PUBLIC ----------------------------
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string ImdbId { get; set; }
        public string Plot { get; set; }

        // ----- INTERNAL -------------------------
        public string FileName { get; set; }
        [field: NonSerialized] public string BasePath;
        public int Id { get; set; }
        public string[] AlternativeNames { get; set; }
        public string PosterPath { get; set; }
    }
}
