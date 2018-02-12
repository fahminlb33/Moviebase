using System;
using System.Windows.Forms;
using Moviebase.Core;
using Moviebase.Core.Diagnostics;
using Moviebase.Core.Services;
using Moviebase.Entities;
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
            BindServices();
            CheckComponents();

            // app
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainView());
        }

        public static void BindServices()
        {
            AppKernel = new StandardKernel();
            var appSettings = Settings.Default;

            // components
            AppKernel.Components.Add<IPlanningStrategy, AutoNotifyInterceptorRegistrationStrategy>();
            
            // services
            AppKernel.Bind<ITmdb, ITmdbWebRequest>().To<Tmdb>().InSingletonScope().WithConstructorArgument("apiKey", appSettings.TmdbApiKey); 
            AppKernel.Bind<IGuessit>().To<Guessit>().InSingletonScope();
            AppKernel.Bind<IPersistFileManager>().To<PersistFileManager>().InSingletonScope().WithConstructorArgument("hideFile", appSettings.HidePresistFile);
            AppKernel.Bind<IThumbnailManager>().To<ThumbnailManager>().InSingletonScope();
            AppKernel.Bind<IComponentManager>().To<ComponentManager>().InSingletonScope();
        }

        private static void CheckComponents()
        {
            var comp = AppKernel.Get<IComponentManager>();
            if (comp.IsPythonInstalled().Result && comp.IsGuessItInstalled().Result) return;

            MessageBox.Show(Strings.ComponentMissingMessage, Strings.AppName,
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
