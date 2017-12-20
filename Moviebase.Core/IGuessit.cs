using Moviebase.Entities;

namespace Moviebase.Core
{
    public interface IGuessit
    {
        GuessitResult GuessName(string filename);
        GuessitResult GuessImdbId(string filename);
        GuessitResult RealGuessName(string filename);
    }
}
