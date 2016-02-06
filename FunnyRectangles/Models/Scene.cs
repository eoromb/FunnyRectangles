using FunnyRectangles.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyRectangles.Models
{
    class Scene
    {
        #region Fields and properties
        private readonly object _selectedObjectLock = new object();
        private readonly IGraphicObjectBuilder _graphicObjectBuilder;
        private readonly LinkedList<IGraphicObject> _graphicObjects = new LinkedList<IGraphicObject>();
        private IGraphicObject _selectedGraphicObject;

        public int Width { get; private set; }
        public int Height { get; private set; }

        #endregion

        #region Constructors
        public Scene(int width, int height, IGraphicObjectBuilder graphObjBuilder)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }
            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }
            if (graphObjBuilder == null)
            {
                throw new ArgumentNullException(nameof(graphObjBuilder));
            }
            Width = width;
            Height = height;
            _graphicObjectBuilder = graphObjBuilder;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Creates rectangle using IGraphicObjectBuilder and adds it at scene.
        /// </summary>
        public void AddRectangle()
        {
            lock (_graphicObjects)
            {
                _graphicObjects.AddLast(_graphicObjectBuilder.CreateRectangle());
            }
        }
        /// <summary>
        /// Brings graphic object at point (x;y) in front.
        /// </summary>
        /// <param name="x">X-coordinate</param>
        /// <param name="y">Y-coordinate</param>
        public void BringInFrontObjectAtCoordinates(int x, int y)
        {
            lock (_graphicObjects)
            {
                var node = _graphicObjects.Last;
                while (node != null)
                {
                    if (node.Value.ContainsPoint(x, y))
                    {
                        _graphicObjects.Remove(node);
                        _graphicObjects.AddLast(node);
                        break;
                    }
                    node = node.Previous;
                }
            }
        }
        /// <summary>
        /// Selects graphic objects at point (x;y)
        /// </summary>
        /// <param name="x">X-coordinates</param>
        /// <param name="y">Y-coordinates</param>
        public void SelectObjectAtCoordinates(int x, int y)
        {
            lock (_selectedObjectLock)
            {
                _selectedGraphicObject = GetObjectByCoordinate(x, y);
            }
        }
        /// <summary>
        /// Unselects last selected graphic object
        /// </summary>
        public void ClearSelection()
        {
            lock (_selectedObjectLock)
            {
                _selectedGraphicObject = null;
            }
        }
        /// <summary>
        /// Moves selected graphic object by dx, dy. It is not allowed to move object outside of scene
        /// </summary>
        /// <param name="dx">x offset</param>
        /// <param name="dy">y offset</param>
        public void MoveSelectedObject(int dx, int dy)
        {
            lock (_selectedObjectLock)
            {
                if (_selectedGraphicObject != null)
                {
                    int resDx;
                    int resDy;
                    AdjustDxDy(_selectedGraphicObject, dx, dy, out resDx, out resDy);
                    _selectedGraphicObject?.Move(resDx, resDy);
                }
            }
        }
        private void AdjustDxDy(IGraphicObject graphicObject, int dx, int dy, out int resDx, out int resDy)
        {
            var boundRect = graphicObject.GetBoundRectangle();
            boundRect.Offset(dx, dy);
            if (dx < 0)
            {
                resDx = boundRect.Left < 0 ? dx - boundRect.Left : dx;
            }
            else
            {
                resDx = boundRect.Right > Width ? dx - (boundRect.Right - Width) : dx;
            }
            if (dy < 0)
            {
                resDy = boundRect.Top < 0 ? dy - boundRect.Top : dy;
            }
            else
            {
                resDy = boundRect.Bottom > Height ? dy - (boundRect.Bottom - Height) : dy;
            }
        }
        /// <summary>
        /// Draws all graphic objects which region is intersected with clipping rectangle
        /// </summary>
        /// <param name="graphics">Graphics to draw at</param>
        /// <param name="clipRectangle">Clipping rectangle</param>
        public void Draw(Graphics graphics, Rectangle clipRectangle)
        {
            lock (_graphicObjects)
            {
                foreach (var graphicObject in _graphicObjects)
                {
                    graphicObject.Draw(graphics, clipRectangle);
                }
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Returns graphic object at point (x;y)
        /// </summary>
        /// <param name="x">X-coordinate</param>
        /// <param name="y">Y-coordinate</param>
        /// <returns></returns>
        private IGraphicObject GetObjectByCoordinate(int x, int y)
        {
            lock (_graphicObjects)
            {
                return _graphicObjects.LastOrDefault(go => go.ContainsPoint(x, y));
            }
        }
        #endregion
    }
}
