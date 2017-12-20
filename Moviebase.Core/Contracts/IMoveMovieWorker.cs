using System.Collections.Generic;

namespace Moviebase.Core.Contracts
{
    public interface IMoveMovieWorker : IWorker
    {
        string AnalyzePath { get; set; }
        List<string> FileExtensions { get; set; }
    }
}
