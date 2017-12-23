namespace Moviebase.Core.Contracts
{
    public interface IDirectoryAnalyzeWorker : IReturningWorker<MovieEntryState>
    {
        string AnalyzePath { get; set; }
    }
}
