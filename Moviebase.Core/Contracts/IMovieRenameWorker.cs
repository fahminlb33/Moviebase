using System.Collections.Generic;
using Moviebase.Entities;

namespace Moviebase.Core.Contracts
{
    public interface IMovieRenameWorker : INonReturningWorker
    {
        List<MovieEntry> MovieEntries { get; set; }
        string FileRenamePattern { get; set; }
        string FolderRenamePattern { get; set; }
        bool SwapThe { get; set; }
    }
}
