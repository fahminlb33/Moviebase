using System.Threading.Tasks;

namespace Moviebase.Core
{
    public class ComponentManager : IComponentManager
    {
        public async Task<bool> CheckPythonInstallation()
        {
            var result = await AsyncProcess.StartWithOuput("python", "-V", ProcessRedirectStream.StandardError);
            return result.Contains("Python");
        }

        public async Task<bool> CheckGuessItInstallation()
        {
            var result = await AsyncProcess.StartWithOuput("guessit", "--version", ProcessRedirectStream.StandardOuput);
            return result.Contains("GuessIt");
        }
    }
}
