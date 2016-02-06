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

            var sceneWidth = 1000;
            var sceneHeight = 700;
            var minRectWidth = 50;
            var minRectHeight = 50;
            var scene = new Scene(sceneWidth, sceneHeight, new RandomGraphicObjectBuilder(sceneWidth, sceneHeight, minRectWidth, minRectHeight),
                new SimpleRectangleOffsetsAdjuster(sceneWidth, sceneHeight));
            var mainWnd = new MainWindow();
            var mainWndController = new MainWindowController(mainWnd, scene);
            mainWnd.SetController(mainWndController);
            try
            {
                Application.Run(mainWnd);
            }
            catch
            {
                // Log something
            }
        }

       
    }
}
