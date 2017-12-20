using System.Collections.Generic;

// ReSharper disable InconsistentNaming
namespace Moviebase.Entities.Web
{
    public class PosterFindRoot
    {
        public int id { get; set; }
        public List<Poster> posters { get; set; }
    }
}
