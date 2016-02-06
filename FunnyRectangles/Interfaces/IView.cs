using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyRectangles.Interfaces
{
    interface IView
    {
        void SetSize(int width, int height);
        void InvalidateSceneRectangle(Rectangle rectangleToInvalidate);
        Rectangle TranslatePageRectangleIntoScene(Rectangle pageRectangle);
        void TranslateCoordinatesPageIntoScene(int x, int y, out int sceneX, out int sceneY);
    }
}
