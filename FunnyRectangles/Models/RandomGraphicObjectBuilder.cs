using FunnyRectangles.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyRectangles.Models
{
    class RandomGraphicObjectBuilder : IGraphicObjectBuilder
    {
        #region Constants
        private int ColorComponentMax = 255;
        #endregion

        #region Fields and properties
        private int _sceneWidth;
        public int SceneWidth
        {
            get { return _sceneWidth; }
            private set
            {
                _sceneWidth = value;
                SetMaxRectangleX();
            }
        }
        private int _sceneHeight;
        public int SceneHeight
        {
            get { return _sceneHeight; }
            private set
            {
                _sceneHeight = value;
                SetMaxRectangleY();
            }
        }
        private int _minRectangleWidth;
        public int MinRectangleWidth
        {
            get { return _minRectangleWidth; }
            private set
            {
                _minRectangleWidth = value;
                SetMaxRectangleX();
            }
        }
        private int _minRectangleHeight;
        public int MinRectangleHeight
        {
            get { return _minRectangleHeight; }
            private set
            {
                _minRectangleHeight = value;
                SetMaxRectangleY();
            }
        }

        private int _maxRectangleX;
        private int _maxRectangleY;
        #endregion

        #region Constructors
        public RandomGraphicObjectBuilder(int sceneWidth, int sceneHeight, int minRectangleWidth, int minRectangleHeight)
        {
            CheckConstructorArgumentsValidity(sceneWidth, sceneHeight, minRectangleWidth, minRectangleHeight);

            SceneWidth = sceneWidth;
            SceneHeight = sceneHeight;
            MinRectangleHeight = minRectangleHeight;
            MinRectangleWidth = minRectangleWidth;
        }
        #endregion

        #region Private methods
        private void SetMaxRectangleX()
        {
            _maxRectangleX = _sceneWidth - _minRectangleWidth;
        }
        private void SetMaxRectangleY()
        {
            _maxRectangleY = _sceneHeight - _minRectangleHeight;
        }
        private void CheckConstructorArgumentsValidity(int sceneWidth, int sceneHeight, int minRectangleWidth, int minRectangleHeight)
        {
            if (sceneWidth < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sceneWidth));
            }
            if (sceneHeight < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sceneHeight));
            }
            if (minRectangleWidth < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minRectangleWidth));
            }
            if (minRectangleHeight < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minRectangleHeight));
            }
            if (minRectangleWidth >= sceneWidth)
            {
                throw new ArgumentException("Minimum rectangle's width must be less then scene's width", nameof(minRectangleWidth));
            }
            if (minRectangleHeight >= sceneHeight)
            {
                throw new ArgumentException("Minimum rectangle's height must be less then scene's height", nameof(minRectangleHeight));
            }
        }
        #endregion

        #region IGraphicObjectBuilder
        public IGraphicObject CreateRectangle()
        {
            var random = new Random();
            var x = random.Next(0, _maxRectangleX);
            var y = random.Next(0, _maxRectangleY);
            var width = random.Next(_minRectangleWidth, _sceneWidth - x);
            var height = random.Next(_minRectangleHeight, _sceneHeight - y);
            var pen = new Pen(GetRandomColor(random));
            var brush = new SolidBrush(GetRandomColor(random));
            var rectModel = new RectangleModel(x, y, width, height, pen, brush);

            return rectModel;
        }
        #endregion

        #region Private methods
        private Color GetRandomColor(Random random) => Color.FromArgb(random.Next(0, ColorComponentMax), random.Next(0, ColorComponentMax), random.Next(0, ColorComponentMax));

        #endregion
    }
}
