using System;
using System.Windows.Forms;
using Moviebase.Services;
using Moviebase.Views;
using Ninject;

namespace Moviebase
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IGuessit>().To<Guessit>().InSingletonScope();
            kernel.Bind<ITmdb>().To<Tmdb>().InSingletonScope();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MovieRenamerView());
        }
    }
}
