using System.ComponentModel;

namespace Moviebase.Entities
{
    public class MovieEntry
    {
        public MovieEntry(TmdbResult data, string fullPath)
        {
            Data = data;
            FullPath = fullPath;
        }

        [Browsable(false)] public TmdbResult Data;

        [Browsable(false)] public string FullPath;

        [Browsable(false)] public int TmdbId => Data.Id;

        public string Title
        {
            get => Data.Title;
            set => Data.Title = value;
        }
        public int Year
        {
            get => Data.Year;
            set => Data.Year = value;
        }
        public string Genre
        {
            get => Data.Genre;
            set => Data.Genre = value;
        }
        public string ImdbId
        {
            get => Data.ImdbId;
            set => Data.ImdbId = value;
        }
        public string Plot
        {
            get => Data.Plot;
            set => Data.Plot = value;
        }
    }
}
