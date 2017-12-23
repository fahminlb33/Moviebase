using System.Collections.Generic;

namespace Moviebase.Core.Contracts
{
    public interface IThumbnailFolderWorker : INonReturningWorker
    {
        List<string> MovieDirectories { get; set; }
        string PosterName { get; set; }
    }
}
