using System;
using System.ComponentModel;
using System.IO;

namespace Moviebase.Entities
{
    public class MovieEntry : ICloneable
    {
        private static readonly string[] SizeSuffix = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB

        private static string BytesToString(long byteCount)
        {
            if (byteCount == 0) return $"0{SizeSuffix[0]}";
            var bytes = Math.Abs(byteCount);
            var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return $"{Math.Sign(byteCount) * num}{SizeSuffix[place]}";
        }

        public void SetFullPath(string fullPath)
        {
            if (fullPath == null) return;
            FullPath = fullPath;
            Size = BytesToString(new FileInfo(fullPath).Length);
        }

        public void SetData(TmdbResult tmdb)
        {
            TmdbId = tmdb.TmdbId;
            AlternativeNames = tmdb.AlternativeNames;
            PosterPath = tmdb.PosterPath;
            Title = tmdb.Title;
            Year = tmdb.Year;
            Genre = tmdb.Genre;
            ImdbId = tmdb.ImdbId;
            Plot = tmdb.Plot;
        }

        public void SetData(GuessitResult guessit)
        {
            Quality = guessit.ScreenSize;
            Source = guessit.ReleaseGroup;
        }

        public void SetData(MovieEntry data)
        {
            TmdbId = data.TmdbId;
            AlternativeNames = data.AlternativeNames;
            PosterPath = data.PosterPath;
            Title = data.Title;
            Year = data.Year;
            Genre = data.Genre;
            ImdbId = data.ImdbId;
            Plot = data.Plot;
            Quality = data.Quality;
            Source = data.Source;
            FullPath = data.FullPath;
            Size = data.Size;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        [Browsable(false)] public int TmdbId;

        [Browsable(false)] public string[] AlternativeNames;

        [Browsable(false)] public string PosterPath;

        [Browsable(false), NonSerialized] public string FullPath;

        public string Title { get; set; }

        public int Year { get; set; }

        public string Genre { get; set; }

        public string ImdbId { get; set; }

        public string Plot { get; set; }
        
        public string Quality { get; set; }
        
        public string Source { get; set; }
        
        public string Size { get; internal set; }

        public string Subtitle { get; set; }

        public override string ToString()
        {
            return $"{TmdbId}: {Title} ({Year})";
        }
    }
}
