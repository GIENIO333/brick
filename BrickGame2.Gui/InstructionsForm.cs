using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace BrickGame2.Gui
{
    public partial class InstructionsForm : Form
    {
        public InstructionsForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Ładne gradientowe tło, pasujące do menu
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using var brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(10, 10, 25),
                Color.FromArgb(25, 30, 60),
                90f);

            e.Graphics.FillRectangle(brush, this.ClientRectangle);

            // delikatny glow za sekcją power-upów
            var glowRect = new Rectangle(0, 230, this.ClientSize.Width, 140);
            using var glow = new LinearGradientBrush(
                glowRect,
                Color.FromArgb(40, 80, 120),
                Color.Transparent,
                90f);
            e.Graphics.FillRectangle(glow, glowRect);
        }
    }
}
