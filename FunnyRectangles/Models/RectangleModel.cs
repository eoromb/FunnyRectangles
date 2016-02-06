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
    /// Describes a rectangle
    /// </summary>
    class RectangleModel : IGraphicObject, IDisposable
    {
        #region Fields and properties
        private Rectangle _boundingRecangle;
        private Rectangle _rectangle;
        private Brush _brush;
        private Pen _pen;
        private Pen Pen
        {
            get { return _pen; }
            set
            {
                _pen = value;
                var penWidth = (int)(_pen.Width + 1.0F);
                _boundingRecangle.Inflate(penWidth, penWidth);
            }
        }
        #endregion

        #region Constructors
        public RectangleModel(int x, int y, int width, int height, Pen pen, Brush brush)
            : this(new Rectangle(x, y, width, height), pen, brush)
        {

        }
        public RectangleModel(Rectangle rect, Pen pen, Brush brush)
        {
            SetRectangle(rect);
            Pen = pen;
            _brush = brush;
        }
        #endregion

        #region IGraphicObject
        /// <summary>
        /// Offsets rectangle 
        /// </summary>
        /// <param name="dx">Displacement along the x-axis</param>
        /// <param name="dy">Displacement along the y-axis</param>
        public void Offset(int dx, int dy)
        {
            _rectangle.Offset(dx, dy);
            _boundingRecangle.Offset(dx, dy);
        }
        /// <summary>
        /// Draws rectangle on graphics if bounding rectangle intersects with clipping rectangle
        /// </summary>
        /// <param name="graphics">Graphics to draw on</param>
        /// <param name="clipRectangle">Clipping rectangle</param>
        public void Draw(Graphics graphics, Rectangle clipRectangle)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }
            if (_boundingRecangle.IntersectsWith(clipRectangle))
            {
                graphics.FillRectangle(_brush, _rectangle);
                graphics.DrawRectangle(_pen, _rectangle);
            }
        }
        /// <summary>
        /// Check if rectangle contains point
        /// </summary>
        /// <param name="x">Point's x-coordinate</param>
        /// <param name="y">Point's y-coordinate</param>
        /// <returns>Returns true if rectangle contains point otherwise returns false</returns>
        public bool ContainsPoint(int x, int y) => _rectangle.Contains(x, y);
        /// <summary>
        /// Returns bounding rectangle
        /// </summary>
        /// <returns>Bounding rectangle</returns>
        public Rectangle GetBoundRectangle() => _boundingRecangle;
        #endregion

        #region Private methods
        private void SetRectangle(Rectangle rect)
        {
            _rectangle = rect;
            _boundingRecangle = rect;
        }

        #region IDisposable Support
        private bool _bDisposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_bDisposed)
            {
                if (disposing)
                {
                    _pen?.Dispose();
                    _brush?.Dispose();
                }
                _bDisposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        #endregion
    }
}
