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

            MainModel model = new MainModel();
            model.DefaultData();

            MainView mainView = new MainView();

            MainPresenter mainPresenter = new MainPresenter(model, mainView);

            mainPresenter.Run();
            
            //Application.Run(mainView);
        }
    }
}
