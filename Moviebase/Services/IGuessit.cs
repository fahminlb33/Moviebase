using Moviebase.Domain;

namespace Moviebase.Services
{
    public interface IGuessit
    {
        GuessitResult GuessName(string filename);
        string GuessImdbId(string filename);
    }
}