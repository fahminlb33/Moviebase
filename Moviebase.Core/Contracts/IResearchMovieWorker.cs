using System.Windows.Forms;

namespace Moviebase.Core.Contracts
{
    public interface IResearchMovieWorker : IWorker
    {
        int Index { get; set; }
        string FullPath { get; set; }
        Form View { get; set; }
    }
}
