using System.Collections.Generic;

namespace Moviebase.Core.Contracts
{
    public interface IMoveMovieWorker : IReturningWorker<string>
    {
        string AnalyzePath { get; set; }
        List<string> FileExtensions { get; set; }
    }
}
