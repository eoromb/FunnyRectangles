using FunnyRectangles.Interfaces;
using FunnyRectangles.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyRectangles.Controllers
{
    class MainWindowController
    {
        #region Constants
        private const int MinimumMoveDelta = 10;
        #endregion

        #region Fields and properties
        private readonly IView _view;
        private readonly Scene _scene;

        private int _draggingPrevX;
        private int _draggingPrevY;
        private bool _bDragging;
        #endregion

        #region Constructors
        public MainWindowController(IView view, Scene scene)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }
            if (scene == null)
            {
                throw new ArgumentNullException(nameof(scene));
            }
            _view = view;
            _scene = scene;
            _view.SetSize(_scene.Width, _scene.Height);
        }
        #endregion

        #region Public methods
        public void BringInFrontObjectAtCoordinates(int pageX, int pageY)
        {
            int sceneX;
            int sceneY;
            _view.TranslateCoordinatesPageIntoScene(pageX, pageY, out sceneX, out sceneY);
            _view.InvalidateSceneRectangle(_scene.BringInFrontObjectAtCoordinates(sceneX, sceneY));
        }
        public void BeginDragging(int pageX, int pageY)
        {
            int sceneX;
            int sceneY;
            _view.TranslateCoordinatesPageIntoScene(pageX, pageY, out sceneX, out sceneY);
            _scene.SelectObjectAtCoordinates(sceneX, sceneY);
            _bDragging = true;
            _draggingPrevX = pageX;
            _draggingPrevY = pageY;
        }
        public void OffsetOnDragging(int pageX, int pageY)
        {
            if (_bDragging &&
                CheckForMinimumMove(pageX, pageY))
            {
                _view.InvalidateSceneRectangle(_scene.OffsetSelectedObject(pageX - _draggingPrevX, pageY - _draggingPrevY));
                _draggingPrevX = pageX;
                _draggingPrevY = pageY;
            }
        }
        public void EndDragging()
        {
            _bDragging = false;
            _draggingPrevX = -1;
            _draggingPrevY = -1;
            _scene.ClearSelection();
        }
        public void AddRectangle()
        {
            _view.InvalidateSceneRectangle(_scene.AddRectangle());
        }
        public void Draw(Graphics graphics, Rectangle clippingRectInPageCoordinates)
        {
            var clippingRectWithSceneCoordinates = _view.TranslatePageRectangleIntoScene(clippingRectInPageCoordinates);
            _scene.Draw(graphics, clippingRectWithSceneCoordinates);
        }
        #endregion

        #region Private methods
        private bool CheckForMinimumMove(int x, int y) => Math.Abs(x - _draggingPrevX) > MinimumMoveDelta || Math.Abs(y - _draggingPrevY) > MinimumMoveDelta;
        #endregion
    }
}
