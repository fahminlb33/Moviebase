using System.Windows.Forms;

namespace Moviebase.Core.Contracts
{
    public interface IResearchMovieWorker : IReturningWorker<ResearchMovieEntryState>
    {
        int Index { get; set; }
        string FullPath { get; set; }
        Form View { get; set; }
    }
}
