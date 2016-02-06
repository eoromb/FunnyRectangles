using System.Drawing;

namespace FunnyRectangles.Interfaces
{
    interface IGraphicObject
    {
        void Offset(int dx, int dy);
        void Draw(Graphics graphics, Rectangle clipRectangle);
        bool ContainsPoint(int x, int y);
        Rectangle GetBoundRectangle();
    }
}
