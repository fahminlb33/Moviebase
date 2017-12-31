using System.Collections.Generic;
using Moviebase.Entities;

namespace Moviebase.Core.Contracts
{
    public interface IPosterDownloadWorker : INonReturningWorker
    {
        List<MovieEntry> MovieEntries { get; set; }
        string FileName { get; set; }
        bool OverwritePoster { get; set; }
    }
}
