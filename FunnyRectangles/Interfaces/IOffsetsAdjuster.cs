using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyRectangles.Interfaces
{
    interface IOffsetsAdjuster
    {
        void AdjustOffsets(IGraphicObject grahicObject, int dx, int dy, out int resDx, out int resDy);
    }
}
