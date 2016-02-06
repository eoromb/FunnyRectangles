using FunnyRectangles.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //if (e.ClipRectangle.Left - AutoScrollPosition.X < c_maxX &&
            //            e.ClipRectangle.Top - AutoScrollPosition.Y < c_maxY)
            //{
            var graphics = e.Graphics;
            graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);
            _scene.Draw(graphics, e.ClipRectangle);
            //graphics.DrawPolygon(_rombPen, _romb);
            //graphics.DrawPolygon(_trianglePen, _triangle);
            //graphics.DrawString(_textToDraw, _courierFont, _textBrush, 10, 10);
            //}
            base.OnPaint(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _scene.BringInFrontObjectAtCoordinates(e.X, e.Y);
            _scene.SelectObjectAtCoordinates(e.X, e.Y);
            Invalidate();
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
                var invalidatedRect = _scene.MoveSelectedObject(e.X - _prevX, e.Y - _prevy);
                _prevX = e.X;
                _prevy = e.Y;
                Invalidate(invalidatedRect);
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
