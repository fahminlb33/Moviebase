using System.Threading.Tasks;
using Moviebase.Entities;

namespace Moviebase.Core.Services
{
    public interface IGuessit
    {
        Task<GuessitResult> GuessName(string filename);
        Task<GuessitResult> GuessImdbId(string filename);
        Task<GuessitResult> RealGuessName(string filename);
    }
}
