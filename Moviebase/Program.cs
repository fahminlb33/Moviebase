using System;
using System.Linq;
using System.Windows.Forms;
using BlastMVP;
using Moviebase.Core;
using Moviebase.Core.Contracts;
using Moviebase.Core.Workers;
using Moviebase.Properties;
using Moviebase.Views;
using Ninject;

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

            // services
            AppKernel.Rebind<ITmdb>().To<Tmdb>().InSingletonScope().WithConstructorArgument("apiKey", appSettings.TmdbApiKey);
            AppKernel.Rebind<IGuessit>().To<Guessit>().InSingletonScope();
            AppKernel.Rebind<IPersistentDataManager>().To<PersistentDataManager>().InSingletonScope();
            AppKernel.Rebind<IThumbnailFolder>().To<ThumbnailFolder>().InSingletonScope();
            AppKernel.Rebind<IComponentManager>().To<ComponentManager>().InSingletonScope();

            // workers
            AppKernel.Rebind<IMoveMovieWorker>().To<MoveMoviesWorker>();
            AppKernel.Rebind<IMovieRenameWorker>().To<MovieRenameWorker>();
            AppKernel.Rebind<IDirectoryAnalyzeWorker>().To<DirectoryAnalyzeWorker>();
            AppKernel.Rebind<IMovieFetchWorker>().To<MovieFetchWorker>();
            AppKernel.Rebind<IPosterDownloadWorker>().To<PosterDownloadWorker>();
            AppKernel.Rebind<ISavePresistDataWorker>().To<SavePresistDataWorker>();
            AppKernel.Rebind<IResearchMovieWorker>().To<ResearchMovieWorker>();
            AppKernel.Rebind<ICsvExportWorker>().To<CsvExportWorker>();
            AppKernel.Rebind<IThumbnailFolderWorker>().To<ThumbnailFolderWorker>();

            // configure 
            var pdm = AppKernel.Get<IPersistentDataManager>();
            pdm.FileExtensions = appSettings.MovieExtensions.Cast<string>().ToArray();
            pdm.PersistentFileName = Commons.PersistentFileName;
            pdm.HidePresistFile = appSettings.HidePresistFile;
        }
    }
}
