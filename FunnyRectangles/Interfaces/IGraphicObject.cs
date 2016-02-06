using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyRectangles.Interfaces
{
    interface IGraphicObject
    {
        void Move(int dx, int dy);
        void Draw(Graphics graphics);
        bool ContainsPoint(int x, int y);
    }
}
