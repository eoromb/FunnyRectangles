using FunnyRectangles.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyRectangles.Models
{
    class RectangleModel : IGraphicObject
    {
        #region Fields and properties
        private Rectangle _rectangle;
        private Brush _brush;
        private Pen _pen;
        #endregion

        #region Constructors
        public RectangleModel(int x, int y, int width, int height, Pen pen, Brush brush)
            : this(new Rectangle(x, y, width, height), pen, brush)
        {

        }
        public RectangleModel(Rectangle rect, Pen pen, Brush brush)
        {
            _rectangle = rect;
            _pen = pen;
            _brush = brush;
        }
        #endregion

        #region IGraphicObject
        public void Move(int dx, int dy)
        {
            _rectangle.Offset(dx, dy);
        }
        public void Draw(Graphics graphics, Rectangle clipRectangle)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }
            if (_rectangle.IntersectsWith(clipRectangle))
            {
                graphics.DrawRectangle(_pen, _rectangle);
                graphics.FillRectangle(_brush, _rectangle);
            }
        }
        public bool ContainsPoint(int x, int y) => _rectangle.Contains(x, y);
        public Rectangle GetBoundRectangle() => _rectangle;
        #endregion
    }
}
