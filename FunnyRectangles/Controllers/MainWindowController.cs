using FunnyRectangles.Interfaces;
using FunnyRectangles.Models;
using System;
using System.Drawing;

namespace FunnyRectangles.Controllers
{
    /// <summary>
    /// Controller for main window. Makes all necessary operations
    /// </summary>
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
        /// <summary>
        /// Brings graphic object at point (x;y) in front.
        /// </summary>
        /// <param name="pageX">X-coordinate in window coordinate system</param>
        /// <param name="pageY">Y-coordinate in window coordinate system</param>
        public void BringInFrontObjectAtCoordinates(int pageX, int pageY)
        {
            int sceneX;
            int sceneY;
            _view.TranslateCoordinatesPageIntoScene(pageX, pageY, out sceneX, out sceneY);
            _view.InvalidateSceneRectangle(_scene.BringInFrontObjectAtCoordinates(sceneX, sceneY));
        }
        /// <summary>
        /// Begins dragging operation by selecting graphics object
        /// </summary>
        /// <param name="pageX">X-coordinate of graphic object in window coordinate system</param>
        /// <param name="pageY">Y-coordinate of graphic in window coordinate system</param>
        public void BeginDragging(int pageX, int pageY)
        {
            int sceneX;
            int sceneY;
            _view.TranslateCoordinatesPageIntoScene(pageX, pageY, out sceneX, out sceneY);
            if (_scene.SelectObjectAtCoordinates(sceneX, sceneY))
            {
                _bDragging = true;
                _draggingPrevX = pageX;
                _draggingPrevY = pageY;
            }
        }
        /// <summary>
        /// Offsets selected object during dragging operation
        /// </summary>
        /// <param name="pageX">X-coordinate of current object's position in window coordinate system</param>
        /// <param name="pageY">Y-coordinate of current object's position in window coordinate system</param>
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
        /// <summary>
        /// Ends dragging operation
        /// </summary>
        public void EndDragging()
        {
            _bDragging = false;
            _draggingPrevX = -1;
            _draggingPrevY = -1;
            _scene.ClearSelection();
        }
        /// <summary>
        /// Add new rectangle into scene
        /// </summary>
        public void AddRectangle()
        {
            _view.InvalidateSceneRectangle(_scene.AddRectangle());
        }
        /// <summary>
        /// Draws all graphic objects which region is intersected with clipping rectangle
        /// </summary>
        /// <param name="graphics">Graphics to draw at</param>
        /// <param name="clippingRectInPageCoordinates">Clipping rectangle</param>
        public void Draw(Graphics graphics, Rectangle clippingRectInPageCoordinates)
        {
            var clippingRectWithSceneCoordinates = _view.TranslatePageRectangleIntoScene(clippingRectInPageCoordinates);
            _scene.Draw(graphics, clippingRectWithSceneCoordinates);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Check if offset is enought  for processing a dragging operation
        /// </summary>
        /// <param name="x">X-coordinate of current object's position in window coordinate system</param>
        /// <param name="y">Y-coordinate of current object's position in window coordinate system</param>
        /// <returns></returns>
        private bool CheckForMinimumMove(int x, int y) => Math.Abs(x - _draggingPrevX) > MinimumMoveDelta || Math.Abs(y - _draggingPrevY) > MinimumMoveDelta;
        #endregion
    }
}
