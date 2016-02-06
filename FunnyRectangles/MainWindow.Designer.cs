namespace FunnyRectangles
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddRectangle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddRectangle
            // 
            this.btnAddRectangle.Location = new System.Drawing.Point(274, 118);
            this.btnAddRectangle.Name = "btnAddRectangle";
            this.btnAddRectangle.Size = new System.Drawing.Size(75, 23);
            this.btnAddRectangle.TabIndex = 0;
            this.btnAddRectangle.Text = "Add rectangle";
            this.btnAddRectangle.UseVisualStyleBackColor = true;
            this.btnAddRectangle.Click += new System.EventHandler(this.btnAddRectangle_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 555);
            this.Controls.Add(this.btnAddRectangle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FunnyRectangles";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddRectangle;
    }
}

