using System.Collections.Generic;
using Moviebase.Entities;

namespace Moviebase.Core.Contracts
{
    public interface IPosterDownloadWorker : IWorker
    {
        List<MovieEntryFacade> MovieEntries { get; set; }
        string FileName { get; set; }
        bool OverwritePoster { get; set; }
    }
}
