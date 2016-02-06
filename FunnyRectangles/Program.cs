using FunnyRectangles.Controllers;
using FunnyRectangles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunnyRectangles
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          //  AppDomain.CurrentDomain.UnhandledException

            var sceneWidth = 1000;
            int sceneHeight = 500;
            var scene = new Scene(sceneWidth, sceneHeight, new RandomGraphicObjectBuilder(sceneWidth, sceneHeight, 50, 50), new SimpleRectangleOffsetsAdjuster(sceneWidth, sceneHeight));
            var mainWnd = new MainWindow();
            var mainWndController = new MainWindowController(mainWnd, scene);
            mainWnd.SetController(mainWndController);
            Application.Run(mainWnd);
        }
    }
}
