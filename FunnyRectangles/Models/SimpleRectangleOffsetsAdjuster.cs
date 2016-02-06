using FunnyRectangles.Interfaces;
using System;

namespace FunnyRectangles.Models
{
    /// <summary>
    /// Adjusts offset of graphic objects using simple algorithm: bounding rectangle of graphic object must not go outside of Rectangle(0, 0, Width, Height).
    /// </summary>
    class SimpleRectangleOffsetsAdjuster : IOffsetsAdjuster
    {
        #region Fields and properties
        public int Width { get; private set; }
        public int Height { get; private set; }
        #endregion

        #region Constructors
        public SimpleRectangleOffsetsAdjuster(int width, int height)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }
            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }
            Width = width;
            Height = height;
        }
        #endregion

        #region ICoordinateAdjuster
        /// <summary>
        /// Adjusts offsets of graphic object.
        /// </summary>
        /// <param name="graphicObject">Graphic object to process</param>
        /// <param name="dx">Displacement along the x-axis</param>
        /// <param name="dy">Displacement along the y-axis</param>
        /// <param name="resDx">Resulting displacement along the x-axis</param>
        /// <param name="resDy">Resulting displacement along the y-axis</param>
        public void AdjustOffsets(IGraphicObject graphicObject, int dx, int dy, out int resDx, out int resDy)
        {
            if (graphicObject == null)
            {
                throw new ArgumentNullException(nameof(graphicObject));
            }
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
        #endregion
    }
}
