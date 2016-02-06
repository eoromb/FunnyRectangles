using FunnyRectangles.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyRectangles.Models
{
    class Scene
    {
        #region Fields and properties
        private readonly IGraphicObjectBuilder _graphicObjectBuilder;
        private readonly LinkedList<IGraphicObject> _graphicObjects = new LinkedList<IGraphicObject>();
        #endregion

        #region Constructors
        public Scene(IGraphicObjectBuilder graphObjBuilder)
        {
            if (graphObjBuilder == null)
            {
                throw new ArgumentNullException(nameof(graphObjBuilder));
            }
            _graphicObjectBuilder = graphObjBuilder;
        }
        #endregion

        #region Public methods
        public void AddRectangle()
        {
            _graphicObjects.AddLast(_graphicObjectBuilder.CreateRectangle());
        }
        #endregion

        #region Private methods
        private IGraphicObject GetObjectByCoordinate(int x, int y) => _graphicObjects.LastOrDefault(go => go.ContainsPoint(x, y));
        #endregion
    }
}
