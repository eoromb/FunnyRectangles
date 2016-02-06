using FunnyRectangles.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyRectangles.Models
{
    /// <summary>
    /// Describe the scene which contains a set of graphic objects. Support several methods which help to manage this objects.
    /// </summary>
    class Scene
    {
        #region Fields and properties
        private readonly object _selectedObjectLock = new object();
        private readonly IOffsetsAdjuster _offsetAdjuster;
        private readonly IGraphicObjectBuilder _graphicObjectBuilder;
        private readonly LinkedList<IGraphicObject> _graphicObjects = new LinkedList<IGraphicObject>();
        private IGraphicObject _selectedGraphicObject;

        public int Width { get; private set; }
        public int Height { get; private set; }

        #endregion

        #region Constructors
        public Scene(int width, int height, IGraphicObjectBuilder graphObjBuilder, IOffsetsAdjuster offsetAdjuster)
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
            if (offsetAdjuster == null)
            {
                throw new ArgumentNullException(nameof(offsetAdjuster));
            }
            Width = width;
            Height = height;
            _graphicObjectBuilder = graphObjBuilder;
            _offsetAdjuster = offsetAdjuster;
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
        /// <returns>Returns bounding rectangle that should be invalidated</returns>
        public Rectangle BringInFrontObjectAtCoordinates(int x, int y)
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
                        return node.Value.GetBoundRectangle();
                        
                    }
                    node = node.Previous;
                }
            }
            return Rectangle.Empty;
        }
        /// <summary>
        /// Selects graphic objects at point (x;y)
        /// </summary>
        /// <param name="x">X-coordinate</param>
        /// <param name="y">Y-coordinate</param>
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
        /// Offsets selected graphic object. It is not allowed to offset object outside of scene
        /// </summary>
        /// <param name="dx">Displacement along the x-axis</param>
        /// <param name="dy">Displacement along the y-axis</param>
        /// <returns>Returns bounding rectangle that should be invalidated</returns>
        public Rectangle OffsetSelectedObject(int dx, int dy)
        {
            lock (_selectedObjectLock)
            {
                if (_selectedGraphicObject != null)
                {
                    int resDx;
                    int resDy;
                    var initialboundingRect = _selectedGraphicObject.GetBoundRectangle();
                    //initialboundingRect.Inflate(1, 1);
                    _offsetAdjuster.AdjustOffsets(_selectedGraphicObject, dx, dy, out resDx, out resDy);
                    _selectedGraphicObject.Offset(resDx, resDy);
                    var resultingboundingRect = _selectedGraphicObject.GetBoundRectangle();
                    //resultingboundingRect.Inflate(1, 1);
                    return Rectangle.Union(initialboundingRect, resultingboundingRect);
                }
                return Rectangle.Empty;
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
