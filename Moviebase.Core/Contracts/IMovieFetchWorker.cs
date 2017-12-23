using System.Collections.Generic;

namespace Moviebase.Core.Contracts
{
    public interface IMovieFetchWorker : IReturningWorker<MovieEntryState>
    {
        List<string> AnalyzeItems { get; set; }
    }
}
