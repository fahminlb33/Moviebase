using System.Threading.Tasks;

namespace Moviebase.Core.Diagnostics
{
    public class ComponentManager : IComponentManager
    {
        public async Task<bool> IsPythonInstalled()
        {
            var result = await AsyncProcess.StartWithOuput("python", "-V", ProcessRedirectStream.StandardError);
            return result.Contains("Python");
        }

        public async Task<bool> IsGuessItInstalled()
        {
            var result = await AsyncProcess.StartWithOuput("guessit", "--version", ProcessRedirectStream.StandardOuput);
            return result.Contains("GuessIt");
        }
    }
}
