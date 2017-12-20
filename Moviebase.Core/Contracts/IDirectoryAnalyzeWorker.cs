namespace Moviebase.Core.Contracts
{
    public interface IDirectoryAnalyzeWorker : IWorker
    {
        string AnalyzePath { get; set; }
    }
}
