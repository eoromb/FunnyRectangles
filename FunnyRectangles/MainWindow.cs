using FunnyRectangles.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FunnyRectangles
{
    partial class MainWindow : Form
    {
        #region Constants
        private const int MinimumMoveDelta = 10;
        #endregion

        #region Fields and properties
        private int _prevX;
        private int _prevy;
        private bool _bDragging;
        private Scene _scene; 
        #endregion

        #region Constructors
        public MainWindow(Scene scene)
        {
            if (scene == null)
            {
                throw new ArgumentNullException(nameof(scene));
            }
            InitializeComponent();
            _scene = scene;

            this.ClientSize = new Size(_scene.Width, _scene.Height);
            this.AutoScrollMinSize = new Size(_scene.Width, _scene.Height);
        }
        #endregion

        #region Overrides
        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var clippingRectWithSceneCoordinates = e.ClipRectangle;
            clippingRectWithSceneCoordinates.Offset(-AutoScrollPosition.X, -AutoScrollPosition.Y);
            graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);
            _scene.Draw(graphics, /*e.ClipRectangle*/clippingRectWithSceneCoordinates);
          
            base.OnPaint(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            var rectangleToInvalidate = _scene.BringInFrontObjectAtCoordinates(e.X, e.Y);
            _scene.SelectObjectAtCoordinates(e.X, e.Y);
            if (rectangleToInvalidate != Rectangle.Empty)
            {
                Invalidate(rectangleToInvalidate);
            }
            _bDragging = true;
            _prevX = e.X;
            _prevy = e.Y;
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _bDragging = false;
            _prevX = -1;
            _prevy = -1;
            _scene.ClearSelection();
            base.OnMouseUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_bDragging &&
                CheckForMinimumMove(e.X, e.Y))
            {
                var invalidatedRect = _scene.OffsetSelectedObject(e.X - _prevX, e.Y - _prevy);
                _prevX = e.X;
                _prevy = e.Y;
                if (invalidatedRect != Rectangle.Empty)
                {
                    Invalidate(invalidatedRect);
                }
            }
            base.OnMouseMove(e);
        }
        #endregion

        #region Events handlers
        private void btnAddRectangle_Click(object sender, EventArgs e)
        {
            _scene.AddRectangle();
            Invalidate();
        }
        #endregion

        #region Private methods
        private bool CheckForMinimumMove(int x, int y) => Math.Abs(x - _prevX) > 10 || Math.Abs(y - _prevy) > 10; 
        #endregion
    }
}
