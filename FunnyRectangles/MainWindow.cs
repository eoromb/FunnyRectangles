using FunnyRectangles.Controllers;
using FunnyRectangles.Interfaces;
using FunnyRectangles.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FunnyRectangles
{
    partial class MainWindow : Form, IView
    {
        #region Fields and properties
        private MainWindowController _wndController;
        #endregion

        #region Constructors
        public MainWindow()
        {
           
            InitializeComponent();
        }
        #endregion

        #region Public methods
        public void SetController(MainWindowController wndController)
        {
            if (wndController == null)
            {
                throw new ArgumentNullException(nameof(wndController));
            }
            _wndController = wndController;
        }
        #endregion

        #region Private methods
        private void CheckOperationValidity()
        {
            if (_wndController == null)
            {
                throw new InvalidOperationException("Window controller has not been set.");
            }
        }
        #endregion

        #region Overrides
        protected override void OnPaint(PaintEventArgs e)
        {
            CheckOperationValidity();
            var graphics = e.Graphics;
            graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);
            _wndController.Draw(graphics, e.ClipRectangle);

            base.OnPaint(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            CheckOperationValidity();

            try
            {
                _wndController.BringInFrontObjectAtCoordinates(e.X, e.Y);
            }
            catch (Exception ex)    // Shouldn't do like this in real application
            {
                MessageBox.Show($"Unable to bring object in fron. {ex.Message}", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                _wndController.BeginDragging(e.X, e.Y);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to begin dragging. {ex.Message}", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            CheckOperationValidity();
            try
            {
                _wndController.EndDragging();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to end dragging. {ex.Message}", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            base.OnMouseUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            CheckOperationValidity();

            try
            {
                _wndController.OffsetOnDragging(e.X, e.Y);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while mouse move. {ex.Message}", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            base.OnMouseMove(e);
        }
        #endregion

        #region Events handlers
        private void btnAddRectangle_Click(object sender, EventArgs e)
        {
            CheckOperationValidity();

            try
            {
                _wndController.AddRectangle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to add rectangle. {ex.Message}", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region IView
        public void SetSize(int width, int height)
        {
            ClientSize = new Size(width, height);
            AutoScrollMinSize = ClientSize;
            MaximumSize = ClientSize;
        }
        public void InvalidateSceneRectangle(Rectangle rectangleToInvalidate)
        {
            if (rectangleToInvalidate != Rectangle.Empty)
            {
                rectangleToInvalidate.Offset(AutoScrollPosition.X, AutoScrollPosition.Y);
                Invalidate(rectangleToInvalidate);
            }
        }
        public Rectangle TranslatePageRectangleIntoScene(Rectangle pageRectangle)
        {
            var sceneRectangle = pageRectangle;
            sceneRectangle.Offset(-AutoScrollPosition.X, -AutoScrollPosition.Y);
            return sceneRectangle;
        }

        public void TranslateCoordinatesPageIntoScene(int x, int y, out int sceneX, out int sceneY)
        {
            sceneX = x - AutoScrollPosition.X;
            sceneY = y - AutoScrollPosition.Y;
        }
        #endregion
    }
}
