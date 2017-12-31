using System.ComponentModel;

namespace Moviebase.Entities
{
    public class MovieEntry
    {
        public MovieEntry(TmdbResult data, string fullPath)
        {
            InternalMovieData = data;
            FullPath = fullPath;
        }

        [Browsable(false)] public TmdbResult InternalMovieData;

        [Browsable(false)] public string FullPath;

        public string Title
        {
            get => InternalMovieData.Title;
            set => InternalMovieData.Title = value;
        }
        public int Year
        {
            get => InternalMovieData.Year;
            set => InternalMovieData.Year = value;
        }
        public string Genre
        {
            get => InternalMovieData.Genre;
            set => InternalMovieData.Genre = value;
        }
        public string ImdbId
        {
            get => InternalMovieData.ImdbId;
            set => InternalMovieData.ImdbId = value;
        }
        public string Plot
        {
            get => InternalMovieData.Plot;
            set => InternalMovieData.Plot = value;
        }
    }
}
