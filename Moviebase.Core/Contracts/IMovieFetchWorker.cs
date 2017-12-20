using System.Collections.Generic;

namespace Moviebase.Core.Contracts
{
    public interface IMovieFetchWorker : IWorker
    {
        List<string> AnalyzeItems { get; set; }
    }
}
