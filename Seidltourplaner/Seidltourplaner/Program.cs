using Seidltourplaner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seidltourplaner
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // MainModel intialisieren und Standarddaten (Lokale in Salzburg) generieren
            MainModel model = new MainModel();
            model.DefaultData();

            // MainView initialisieren
            MainView mainView = new MainView();

            // MainPresenter initialisieren
            MainPresenter mainPresenter = new MainPresenter(model, mainView);

            // MainPresenter starten
            mainPresenter.Run();
        }
    }
}
