using FunnyRectangles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyRectangles.Interfaces
{
    interface IGraphicObjectBuilder
    {
        IGraphicObject CreateRectangle();
    }
}
