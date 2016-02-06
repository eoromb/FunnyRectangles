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
    public partial class MainWindow : Form
    {
        private Scene _scene;
        public MainWindow()
        {
            InitializeComponent();
            _scene = new Scene(Width, Height, new RandomGraphicObjectBuilder(Width, Height, 50, 50));
            this.AutoScrollMinSize = new Size(800, 500);
        }
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
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _scene.ClearSelection();
            base.OnMouseUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //_scene.MoveSelectedObject(e.)
            base.OnMouseMove(e);
        }
        private void btnAddRectangle_Click(object sender, EventArgs e)
        {
            _scene.AddRectangle();
            Invalidate();
        }
    }
}
