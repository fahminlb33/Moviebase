using System.Collections.Generic;

namespace Moviebase.Core.Contracts
{
    public interface IThumbnailFolderWorker : IWorker
    {
        List<string> MovieDirectories { get; set; }
        string PosterName { get; set; }
    }
}
