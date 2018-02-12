using System;
using System.Windows.Forms;
using Moviebase.Core;
using Moviebase.Core.Diagnostics;
using Moviebase.Core.Services;
using Moviebase.Properties;
using Moviebase.Views;
using Ninject;
using Ninject.Extensions.Interception.Planning.Strategies;
using Ninject.Planning.Strategies;

namespace Moviebase
{
    static class Program
    {
        public static StandardKernel AppKernel;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppKernel = new StandardKernel();
            RebindAll();
            
            // app
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainView());
        }

        public static void RebindAll()
        {
            var appSettings = Settings.Default;

            // components
            AppKernel.Components.Add<IPlanningStrategy, AutoNotifyInterceptorRegistrationStrategy>();
            
            // services
            AppKernel.Bind<ITmdb>().To<Tmdb>().InSingletonScope().WithConstructorArgument("apiKey", appSettings.TmdbApiKey); 
            AppKernel.Bind<IGuessit>().To<Guessit>().InSingletonScope();
            AppKernel.Bind<IPersistFileManager>().To<PersistFileManager>().InSingletonScope().WithConstructorArgument("hideFile", appSettings.HidePresistFile);
            AppKernel.Bind<IThumbnailManager>().To<ThumbnailManager>().InSingletonScope();
            AppKernel.Bind<IComponentManager>().To<ComponentManager>().InSingletonScope();
        }
    }
}
